using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FaceRecognizer.Api
{
    /// <summary>
    /// 人脸识别建档请求实体
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// 证件类型: 01-身份证
        /// </summary>
        public string idType { get; set; } = "01";
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
        /// 图片信息
        /// </summary>
        public Image image { get; set; }
        /// <summary>
        /// 图片类型
        /// </summary>
        public string imageType { get; set; }

        public string cardNo { get; set; }
    }
}
