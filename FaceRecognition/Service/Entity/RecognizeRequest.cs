using System.Drawing;

namespace FaceRecognition.Service.Entity
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
    }
}
