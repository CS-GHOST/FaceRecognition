using System.Drawing;

namespace FaceRecognition.Entity
{
    /// <summary>
    /// 配置信息类
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 摄像头索引,默认值为0
        /// </summary>
        public int cameraIndex { get; set; } = 0;

        /// <summary>
        /// 倒计时时间（秒）,默认值为60
        /// </summary>
        public int timeOut { get; set; } = 60;

        /// <summary>
        /// 检测人脸大小占比，,默认值为4
        /// </summary>
        public int detectFaceScaleVal { get; set; } = 4;

        /// <summary>
        /// 图像旋转角度
        /// </summary>
        public int rotateType { get; set; } = 0;

        /// <summary>
        /// 界面位置
        /// </summary>
        public Point location { get; set; }

        /// <summary>
        /// 界面大小
        /// </summary>
        public Size size { get; set; }

        /// <summary>
        /// 平台接口地址
        /// </summary>
        public string apiUrl { get; set; }

        /// <summary>
        /// 接口密钥
        /// </summary>
        public string apiKey { get; set; }
    }
}
