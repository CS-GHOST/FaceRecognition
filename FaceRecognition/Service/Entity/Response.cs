namespace FaceRecognition.Service.Entity
{
    /// <summary>
    /// 公共返回实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> where T : new()
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 成功标志
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 数据实体
        /// </summary>
        public T data { get; set; }
    }
}
