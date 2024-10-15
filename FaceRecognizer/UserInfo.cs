using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceRecognizer
{
    public class UserInfo
    {
        /// <summary>
        /// 证件类型
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
        public string cardNo { get; set; }

    }
}
