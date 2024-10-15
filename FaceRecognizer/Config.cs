using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceRecognizer
{
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
        public int rotateAngle { get; set; } = 0;
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
