using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceRecognizer
{
    public class FaceResult
    {
        /// <summary>
        /// 返回码
        /// 0-成功，1-失败，2-异常，-1-未执行
        /// </summary>
        public string code { get; set; } = "-1";
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public string data { get; set; }
    }
}
