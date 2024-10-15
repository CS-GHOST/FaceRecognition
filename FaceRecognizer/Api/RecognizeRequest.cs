using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FaceRecognizer.Api
{
    /// <summary>
    /// 人脸识别查证请求实体
    /// </summary>
    public class RecognizeRequest
    {
        /// <summary>
        /// 人像信息
        /// </summary>
        public Image file { get; set; }
        /// <summary>
        /// 图像类型
        /// </summary>
        public string faceImageType { get; set; }
    }
}
