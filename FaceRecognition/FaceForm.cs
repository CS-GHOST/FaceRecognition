using AForge.Video.DirectShow;
using FaceRecognition.Entity;
using FaceRecognition.Utils;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace FaceRecognition
{
    /// <summary>
    /// 人脸图像采集界面
    /// </summary>
    internal partial class FaceForm : Form
    {
        #region 参数定义
        /// <summary>
        /// 返回值对象
        /// </summary>
        public FaceResult FaceResult = new FaceResult();
        /// <summary>
        /// 图片处理委托
        /// </summary>
        public Func<Image, FaceResult> ImageHandle;
        /// <summary>
        /// 人脸识别重试次数
        /// </summary>
        public int RetryCount = 0;

        #region 私有字段
        /// <summary>
        /// 配置信息
        /// </summary>
        private readonly Config _config;
        /// <summary>
        /// 倒计时
        /// </summary>
        private int _timeOut = 0;
        /// <summary>
        /// 定时器
        /// </summary>
        private System.Timers.Timer _timer;
        /// <summary>
        /// 视频输入设备信息
        /// </summary>
        private FilterInfoCollection _filterInfoCollection;
        /// <summary>
        /// RGB摄像头设备
        /// </summary>
        private VideoCaptureDevice _videoDevice;
        /// <summary>
        /// 正在处理标志
        /// </summary>
        private bool _isProcessing = false;
        /// <summary>
        /// 人脸框笔画
        /// </summary>
        private readonly Pen _pen = Pens.Red;
        /// <summary>
        /// 当前处理次数
        /// </summary>
        private int _processCount = 0;
        #endregion 私有字段

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

            //隐藏摄像头图像窗口
            rgbVideoSource.Hide();
            this._config = config;
            InitVideo();
            //设置窗体位置大小
            if (config.size.Width != 0)
            {
                this.Location = config.location;
                this.Size = config.size;
                this.WindowState = FormWindowState.Normal;
            }
        }

        /// <summary>
        /// 启动摄像头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FaceForm_Load(object sender, EventArgs e)
        {
            this.TopMost = false;
            //必须保证有可用摄像头
            if (_filterInfoCollection.Count == 0)
            {
                FaceResult.code = "1";
                FaceResult.message = "未检测到摄像头，请确保已安装摄像头或驱动!";
                this.Close();
                return;
            }

            if (rgbVideoSource.IsRunning)
            {
                //关闭摄像头
                if (rgbVideoSource.IsRunning)
                {
                    rgbVideoSource.SignalToStop();
                    rgbVideoSource.Hide();
                }
            }
            else
            {
                rgbVideoSource.Size = new Size((this.Size.Width * 2 / 5), this.Size.Width * 2 / 5);
                rgbVideoSource.Location = new Point(this.Location.X + this.Size.Width / 2 - rgbVideoSource.Size.Width / 2, rgbVideoSource.Location.Y);
                this.labTimeOut.Location = new Point(this.Location.X + this.Size.Width / 2 - 45, this.labTimeOut.Location.Y);

                //显示摄像头控件
                rgbVideoSource.Show();
                //获取filterInfoCollection的总数
                int maxCameraCount = _filterInfoCollection.Count;

                //打开RGB摄像头
                _videoDevice = new VideoCaptureDevice(_filterInfoCollection[_config.cameraIndex < maxCameraCount ? _config.cameraIndex : 0].MonikerString);
                _videoDevice.VideoResolution = _videoDevice.VideoCapabilities[0];
                rgbVideoSource.VideoSource = _videoDevice;
                rgbVideoSource.Start();
            }

            SetDoubleBuffered(this.labTimeOut, true);
            SetDoubleBuffered(this.pnlBottom, true);
            DrawImgRegion();
            //启动倒计时
            ShowTimeOut();
        }

        /// <summary>
        /// 摄像头初始化
        /// </summary>
        private void InitVideo()
        {
            _filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        #endregion

        #region 视频检测

        /// <summary>
        /// RGB摄像头Paint事件，图像显示到窗体上，得到每一帧图像，并进行处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoSource_Paint(object sender, PaintEventArgs e)
        {
            if (!rgbVideoSource.IsRunning)
            {
                return;
            }

            //1、得到当前RGB摄像头下的图片
            Bitmap bitmap = rgbVideoSource.GetCurrentVideoFrame();
            if (bitmap == null)
            {
                return;
            }

            //2、检测最大人脸
            Rectangle rect = ImageTool.GetMaxFaceRect(bitmap);
            if (rect == Rectangle.Empty)
            {
                ShowMessage("未识别到人脸");
                return;
            }

            #region  3、画人脸框
            Graphics g = e.Graphics;
            float offsetX = rgbVideoSource.Width * 1f / bitmap.Width;
            float offsetY = rgbVideoSource.Height * 1f / bitmap.Height;
            float x = rect.X * offsetX;
            float width = rect.Width * offsetX;
            float y = rect.Y * offsetY;
            float height = rect.Height * offsetY;
            //根据Rect进行画框
            g.DrawRectangle(_pen, x, y, width, height);
            #endregion

            //4、判断人脸大小
            if (width != 0 && width < Convert.ToSingle(rgbVideoSource.Width) / Convert.ToSingle(this._config.detectFaceScaleVal))
            {
                ShowMessage("请靠近，并正对摄像头");
                return;
            }

            //5、处理人脸图像
            ShowMessage("正在处理，请稍等");
            if (_isProcessing == false)
            {
                //保证只检测一帧，防止页面卡顿
                _isProcessing = true;
                //异步处理，防止页面卡顿
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    ProcessImage(ImageTool.CropImage(bitmap, rect));
                    _isProcessing = false;
                }));
            }
        }

        /// <summary>
        /// 摄像头播放完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        private void videoSource_PlayingFinished(object sender, AForge.Video.ReasonToFinishPlaying reason)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
            }
            catch (Exception ex)
            {
                LogHelper.Save("摄像头播放完成事件异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 外部图片处理
        /// </summary>
        /// <param name="bitmap"></param>
        private void ProcessImage(Bitmap bitmap)
        {
            _processCount++;
            //bitmap.Save(AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".bmp");

            if (ImageHandle == null)
            {
                return;
            }

            FaceResult = ImageHandle.Invoke(bitmap);
            //成功或超过重试次数，则关闭窗体
            if (FaceResult.code == "0" || _processCount >= RetryCount)
            {
                ShowMessage(FaceResult.message);
                CloseForm();
            }
        }

        #endregion

        #region 界面显示

        /// <summary>
        /// 图像旋转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="image"></param>
        private void rgbVideoSource_NewFrame(object sender, ref Bitmap image)
        {
            if (image != null)
            {
                RotateFlipType pType = RotateFlipType.RotateNoneFlipNone;
                if (_config.rotateType == 0)
                {
                    pType = RotateFlipType.RotateNoneFlipNone;
                }
                else if (_config.rotateType == 90)
                {
                    pType = RotateFlipType.Rotate90FlipNone;
                }
                else if (_config.rotateType == 180)
                {
                    pType = RotateFlipType.Rotate180FlipNone;
                }
                else if (_config.rotateType == 270)
                {
                    pType = RotateFlipType.Rotate270FlipNone;
                }

                // 实时按角度绘制
                image.RotateFlip(pType);
            }
        }

        /// <summary>
        /// 画圆形框
        /// </summary>
        private void DrawImgRegion()
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, rgbVideoSource.Size.Width, rgbVideoSource.Size.Width);
            Region region = new Region(gp);
            rgbVideoSource.Region = region;
            gp.Dispose();
            region.Dispose();
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            try
            {
                if (!this.IsDisposed && this.lblTitle != null && !this.lblTitle.IsDisposed)
                {
                    this.Invoke(new Action(delegate
                    {
                        this.lblTitle.Text = message;
                    }));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Save("显示提示信息异常：" + ex.Message);
            }
        }

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

        #endregion

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
        /// 关闭窗体
        /// </summary>
        private void CloseForm()
        {
            _isProcessing = true;
            try
            {
                if (!this.IsDisposed)
                {
                    Invoke(new Action(delegate
                    {
                        this.Close();
                    }));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Save("关闭窗体异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPre_Click(object sender, EventArgs e)
        {
            _isProcessing = true;
            LogHelper.Save("用户取消操作");
            FaceResult.message = "用户取消操作。";
            FaceResult.code = "1";
            this.Close();
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            _isProcessing = true;
            string msg = string.Empty;
            try
            {
                _timer.Enabled = false;
                _timer.Dispose();
                _timer.Close();

                //关闭摄像头
                if (rgbVideoSource.IsRunning)
                {
                    rgbVideoSource.SignalToStop();
                    rgbVideoSource.Hide();
                }
                //祖传代码，防止线程销毁前线程间调用
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                LogHelper.Save("窗体关闭事件异常：" + ex.Message);
                msg = ex.Message;
            }
            finally
            {
                if (FaceResult.message == "" && FaceResult.code == "-1")
                {
                    FaceResult.message = "操作超时。";
                    FaceResult.code = "1";
                }
                else
                {
                    FaceResult.message += msg;
                }
            }
        }

        #endregion
    }
}
