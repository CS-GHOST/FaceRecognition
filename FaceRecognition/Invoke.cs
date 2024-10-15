using FaceRecognition.Entity;
using FaceRecognition.Service;
using FaceRecognition.Utils;
using System;
using System.Drawing;
using System.IO;

namespace FaceRecognition
{
    /// <summary>
    /// 人脸识别模块操作入口
    /// </summary>
    public class Invoke
    {
        #region 字段
        /// <summary>
        /// 配置信息
        /// </summary>
        static Config _config = new Config();
        /// <summary>
        /// 可用标记
        /// </summary>
        private static bool _canUse = false;
        /// <summary>
        /// 人脸识别服务
        /// </summary>
        static IRecognitionService _recognizeService;
        /// <summary>
        /// 操作类型
        /// </summary>
        private static OpType _opType;
        /// <summary>
        /// 人脸注册信息
        /// </summary>
        private static UserInfo _userInfo;
        #endregion

        /// <summary>
        /// 静态构造方法
        /// </summary>
        static Invoke()
        {
            try
            {
                //初始化配置文件
                string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\FaceRecognitionConfig.ini";
                if (!File.Exists(fileName))
                {
                    throw new Exception($"未找到配置文件: {fileName}");
                }
                string nodeKey = "Config";
                _config.cameraIndex = Convert.ToInt32(IniTool.Read(fileName, nodeKey, "cameraIndex"));
                _config.timeOut = Convert.ToInt32(IniTool.Read(fileName, nodeKey, "timeOut"));
                _config.detectFaceScaleVal = Convert.ToInt32(IniTool.Read(fileName, nodeKey, "detectFaceScaleVal"));
                _config.rotateType = Convert.ToInt32(IniTool.Read(fileName, nodeKey, "rotateType"));
                _config.apiUrl = IniTool.Read(fileName, nodeKey, "apiUrl");
                _config.apiKey = IniTool.Read(fileName, nodeKey, "apiKey");


                string location = IniTool.Read(fileName, nodeKey, "location");
                string size = IniTool.Read(fileName, nodeKey, "size");

                if (!string.IsNullOrWhiteSpace(size) && !string.IsNullOrWhiteSpace(size))
                {
                    _config.location = new Point(Convert.ToInt32(location.Split(',')[0]), Convert.ToInt32(location.Split(',')[1]));
                    _config.size = new Size(Convert.ToInt32(size.Split(',')[0]), Convert.ToInt32(size.Split(',')[1]));
                }
                _canUse = true;
            }
            catch (Exception ex)
            {
                LogHelper.Save("加载配置文件异常：" + ex.Message);
            }

            //初始化人脸识别接口服务
            _recognizeService = new RecognizeService(_config);
        }

        /// <summary>
        /// 获取人脸图像
        /// </summary>
        /// <returns></returns>
        public static FaceResult FaceDetect()
        {
            FaceResult result = new FaceResult();

            if (!_canUse)
            {
                result.code = "1";
                result.message = "功能不可用，请检查配置";
                return result;
            }

            try
            {
                _opType = OpType.FACE_DETECT;
                FaceForm faceForm = new FaceForm(_config);
                faceForm.ImageHandle = ImageHandle;
                faceForm.ShowDialog();
                result = faceForm.FaceResult;
            }
            catch (Exception ex)
            {
                result.code = "2";
                result.message = "处理异常：" + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <param name="userInfo">注册用户信息</param>
        /// <returns></returns>
        public static FaceResult FaceRegister(UserInfo userInfo)
        {
            FaceResult result = new FaceResult();

            if (!_canUse)
            {
                result.code = "1";
                result.message = "功能不可用，请检查配置";
                return result;
            }

            try
            {
                _opType = OpType.FACE_REGISTER;
                _userInfo = userInfo;

                FaceForm faceForm = new FaceForm(_config);
                faceForm.ImageHandle = ImageHandle;
                faceForm.ShowDialog();
                result = faceForm.FaceResult;
            }
            catch (Exception ex)
            {
                result.code = "2";
                result.message = "处理异常：" + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 人脸识别
        /// </summary>
        /// <returns></returns>
        public static FaceResult FaceRecognize()
        {
            FaceResult result = new FaceResult();

            if (!_canUse)
            {
                result.code = "1";
                result.message = "功能不可用，请检查配置";
                return result;
            }

            try
            {
                _opType = OpType.FACE_RECOGNIZE;
                FaceForm faceForm = new FaceForm(_config);
                faceForm.RetryCount = 3;
                faceForm.ImageHandle = ImageHandle;
                faceForm.ShowDialog();
                result = faceForm.FaceResult;
            }
            catch (Exception ex)
            {
                result.code = "2";
                result.message = "处理异常：" + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 图像处理
        /// </summary>
        /// <param name="image">人脸图像</param>
        /// <returns></returns>
        private static FaceResult ImageHandle(Image image)
        {
            FaceResult result = new FaceResult();

            if (_opType == OpType.FACE_REGISTER)
            {
                result = _recognizeService.Register(image, _userInfo);

            }
            else if (_opType == OpType.FACE_RECOGNIZE)
            {
                result = _recognizeService.Recognize(image);
            }
            else
            {
                result.code = "0";
                result.message = "获取人脸图片成功。";
                result.data = image;
            }

            return result;
        }
    }
}
