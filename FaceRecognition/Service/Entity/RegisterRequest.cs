using System.Drawing;

namespace FaceRecognition.Service.Entity
{
    /// <summary>
    /// 人脸识别建档请求实体
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// 证件类型: 01-身份证
        /// </summary>
        public string idType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string idNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string cardNo { get; set; }

        /// <summary>
        /// 图片信息
        /// </summary>
        public Image image { get; set; }
    }
}
