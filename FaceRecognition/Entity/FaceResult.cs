namespace FaceRecognition.Entity
{
    /// <summary>
    /// 返回参数实体
    /// </summary>
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
        public object data { get; set; }
    }
}
