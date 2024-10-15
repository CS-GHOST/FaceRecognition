using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using FaceRecognizer.Api;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRecognizer
{
    internal partial class FaceForm : Form
    {
        #region 参数定义
        /// <summary>
        /// 配置信息
        /// </summary>
        private Config _config;
        /// <summary>
        /// 倒计时
        /// </summary>
        private int _timeOut = 0;
        /// <summary>
        /// 定时器
        /// </summary>
        private System.Timers.Timer _timer;
        /// <summary>
        /// 摄像头句柄
        /// </summary>
        private VideoCapture _capture = null;
        /// <summary>
        /// 处理图像帧
        /// </summary>
        private Mat _frame;
        /// <summary>
        /// 人脸检测引擎
        /// </summary>
        private CascadeClassifier _faceDetect = new CascadeClassifier(@"haarcascade\haarcascade_frontalface_default.xml");
        /// <summary>
        /// 视频宽度
        /// </summary>
        private int _videoFrameWidth = 1500;
        /// <summary>
        /// 正在处理标志
        /// </summary>
        private bool isProcessing = false;

        /// <summary>
        /// 操作类型
        /// </summary>
        public OpType OpType;
        /// <summary>
        /// 注册信息
        /// </summary>
        public UserInfo RegisterUserInfo;
        /// <summary>
        /// 返回结果
        /// </summary>
        public FaceResult FaceResult = null;
        #endregion

        #region 初始化
        /// <summary>
        /// 获取特征值
        /// </summary>
        /// <param name="config"></param>
        public FaceForm(Config config)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.TopMost = false;

            this._config = config;
            FaceResult = new FaceResult();
            _frame = new Mat();

           
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FaceForm_Load(object sender, EventArgs e)
        {
            //控件自动定位
            imgVideoSource.Size = new Size((this.Size.Width * 2 / 5), this.Size.Width * 2 / 5);
            imgVideoSource.Location = new Point(this.Location.X + this.Size.Width / 2 - imgVideoSource.Size.Width / 2, imgVideoSource.Location.Y);
            this.labTimeOut.Location = new Point(this.Location.X + this.Size.Width / 2 - 45, this.labTimeOut.Location.Y);
            DrawImgRegion();
            //启动倒计时
            ShowTimeOut();
            //开启摄像头
            StartVideo();

            SetDoubleBuffered(this.labTimeOut, true);
            SetDoubleBuffered(this.pnlBottom, true);
        }

        #endregion

        /// <summary>
        /// 启动摄像头
        /// </summary>
        private void StartVideo()
        {
            CvInvoke.UseOpenCL = false;
            try
            {
                _capture = new VideoCapture(_config.cameraIndex);
                _capture.SetCaptureProperty(CapProp.FrameWidth, _videoFrameWidth);
                _capture.SetCaptureProperty(CapProp.FrameHeight, _videoFrameWidth);
                _capture.ImageGrabbed += ProcessFrame;
                _capture.Start();
                if (!_capture.IsOpened)
                {
                    FaceResult.code = "1";
                    FaceResult.message = "打开摄像头失败";
                    ShowMessage("打开摄像头失败");
                    _timeOut = 4;
                }
            }
            catch (NullReferenceException ex)
            {
                FaceResult.code = "2";
                FaceResult.message = "打开摄像头异常" + ex.Message;
                ShowMessage("打开摄像头异常");
                _timeOut = 4;
            }
        }

        /// <summary>
        /// 每帧图片处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void ProcessFrame(object sender, EventArgs arg)
        {
            if (isProcessing)
            {
                Console.WriteLine("isProcessing");
                return;
            }
            isProcessing = true;

            if (_capture == null || _capture.Ptr == IntPtr.Zero || !_capture.IsOpened)
            {
                return;
            }
            _capture.Retrieve(_frame, 0);
            imgVideoSource.Image = _frame.Bitmap;
            //旋转
            imgVideoSource.Image.RotateFlip(ConvertRotateFlipType(_config.rotateAngle));

            Rectangle[] recs = _faceDetect.DetectMultiScale(_frame);
            if (recs == null)
            {
                this.Invoke(new Action(delegate
                {
                    this.Close();
                }));
                return;
            }

            if (recs.Length == 0)
            {
                ShowMessage("未识别到人脸");
                isProcessing = false;
                return;
            }

            Rectangle rect = GetMaxRectangle(recs);

            CvInvoke.Rectangle(_frame, rect, new MCvScalar(0, 0, 255));

            if (rect.Width != 0 && rect.Width < Convert.ToSingle(imgVideoSource.Width) / Convert.ToSingle(this._config.detectFaceScaleVal))
            {
                ShowMessage("请靠近，并正对摄像头");
                isProcessing = false;
                return;
            }

            ShowMessage("正在处理,请稍等");

            ProcessOperation(Utils.BitmapDeepClone(_frame.Bitmap));
        }

        private async void ProcessOperation(Bitmap bitmap)
        {
            if (OpType == OpType.FACE_DETECT)
            {
                //FaceResult.code = "0";
                //FaceResult.message = "成功";

                RecognizeApi.RecognizeS(_frame.Bitmap);
            }
            else if (OpType == OpType.FACE_REGISTER)
            {
                RegisterRequest request = new RegisterRequest();
                request.idType = RegisterUserInfo.idType;
                request.idNo = RegisterUserInfo.idNo;
                request.name = RegisterUserInfo.name;
                request.mobile = RegisterUserInfo.mobile;
                request.cardNo = RegisterUserInfo.cardNo;
                request.imageType = "";
                request.image = bitmap;
                bitmap.Save("c:\\" + RegisterUserInfo.idNo + ".bmp");
                var task = RecognizeApi.Register(request);
                var response = task.Result;

            }
            else if (OpType == OpType.FACE_RECOGNIZE)
            {
                RecognizeRequest request = new RecognizeRequest();
                request.faceImageType = "";
                request.file = bitmap;
                var task = RecognizeApi.Recognize(request);
                var response = task.Result;
            }
            else
            {

            }
        }

        /// <summary>
        /// bitmap转mat
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private Mat Bitmap2Mat(Bitmap bitmap)
        {
            Image<Bgr, byte> imageCV = new Image<Bgr, byte>(bitmap);
            Mat mat = imageCV.Mat;
            return mat;
        }

        /// <summary>
        /// 旋转角度转换枚举
        /// </summary>
        /// <param name="rotateAngle"></param>
        /// <returns></returns>
        private RotateFlipType ConvertRotateFlipType(int rotateAngle)
        {
            RotateFlipType type = RotateFlipType.RotateNoneFlipNone;
            switch (rotateAngle)
            {
                case 90:
                    type = RotateFlipType.Rotate90FlipNone;
                    break;
                case 180:
                    type = RotateFlipType.Rotate180FlipNone;
                    break;
                case 270:
                    type = RotateFlipType.Rotate270FlipNone;
                    break;
                default:
                    type = RotateFlipType.RotateNoneFlipNone;
                    break;
            }
            return type;
        }

        /// <summary>
        /// 获取最大人脸框
        /// </summary>
        /// <param name="recs"></param>
        /// <returns></returns>
        private Rectangle GetMaxRectangle(Rectangle[] recs)
        {
            if (recs == null || recs.Length == 0)
            {
                return Rectangle.Empty;
            }
            Rectangle maxRectangle = recs[0];
            for (int i = 1; i < recs.Length; i++)
            {
                if (recs[i].Width > maxRectangle.Width)
                {
                    maxRectangle = recs[i];
                }
            }

            return maxRectangle;
        }

        /// <summary>
        /// 画圆形框
        /// </summary>
        private void DrawImgRegion()
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, imgVideoSource.Size.Width, imgVideoSource.Size.Width);
            Region region = new Region(gp);
            imgVideoSource.Region = region;
            gp.Dispose();
            region.Dispose();
        }

        /// <summary>
        /// 图像旋转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="image"></param>
        private void rgbVideoSource_NewFrame(object sender, ref Bitmap image)
        {
            if (image != null)
            {
                //RotateFlipType pType = RotateFlipType.RotateNoneFlipNone;
                //if (_config.rotateType == 0)
                //{
                //    pType = RotateFlipType.RotateNoneFlipNone;
                //}
                //else if (_config.rotateType == 90)
                //{
                //    pType = RotateFlipType.Rotate90FlipNone;
                //}
                //else if (_config.rotateType == 180)
                //{
                //    pType = RotateFlipType.Rotate180FlipNone;
                //}
                //else if (_config.rotateType == 270)
                //{
                //    pType = RotateFlipType.Rotate270FlipNone;
                //}

                // 实时按角度绘制
                //image.RotateFlip(pType);
            }
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            try
            {
                if (this != null && !this.IsDisposed && this.lblTitle != null && !this.lblTitle.IsDisposed)
                {
                    this.Invoke(new Action(delegate
                    {
                        this.lblTitle.Text = message;
                    }));
                }
            }
            catch (Exception ex)
            {
            }
        }

        #region 倒计时

        /// <summary>
        /// 启动倒计时
        /// </summary>
        private void ShowTimeOut()
        {
            _timeOut = _config.timeOut;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (o, e) =>
            {
                SetTimeOut();
            };
            _timer.Enabled = true;
        }

        /// <summary>
        /// 设置倒计时
        /// </summary>
        private void SetTimeOut()
        {
            _timeOut--;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.labTimeOut.Text = _timeOut.ToString();
                }));
            }
            if (_timeOut <= 0)
            {
                if (FaceResult.code == "-1")
                {
                    FaceResult.code = "1";
                    FaceResult.message = "操作超时。";
                }
                this.Close();
            }
        }

        #endregion

        #region 退出

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPre_Click(object sender, EventArgs e)
        {
            FaceResult.message = "用户取消操作。";
            FaceResult.code = "1";
            this.Close();
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            string msg = string.Empty;
            try
            {
                _timer.Enabled = false;
                _timer.Dispose();
                _timer.Close();
                //关闭摄像头
                if (_capture != null)
                {
                    _capture.Stop();
                    _capture.Dispose();
                    _capture = null;
                }
                //祖传代码，防止线程销毁前线程间调用
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (string.IsNullOrWhiteSpace(FaceResult.message) && FaceResult.code == "-1")
                {
                    FaceResult.message = "用户取消操作。";
                    FaceResult.code = "1";
                }
                else
                {
                    FaceResult.message += msg;
                }
            }
        }
        #endregion

        #region 防止闪烁
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED  
                return cp;
            }
        }

        /// <summary>
        /// 设置控件双缓冲
        /// </summary>
        /// <param name="ctl">控件对象</param>
        /// <param name="value">bool值</param>
        private static void SetDoubleBuffered(Control ctl, bool value)
        {
            try
            {
                ctl.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                         System.Reflection.BindingFlags.NonPublic).SetValue(ctl, value, null);
            }
            catch
            {
            }
        }
        #endregion
    }
}
