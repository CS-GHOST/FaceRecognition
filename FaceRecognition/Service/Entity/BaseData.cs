namespace FaceRecognition.Service.Entity
{
    /// <summary>
    /// 共用基础返回实体
    /// </summary>
    public class BaseData
    {
        /// <summary>
        /// 交易ID
        /// </summary>
        public string transId { get; set; }

        /// <summary>
        /// 返回时间
        /// </summary>
        public string responseTime { get; set; }

        /// <summary>
        /// 业务返回状态
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 信息编码
        /// </summary>
        public string msgCode { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string inParamSize { get; set; }
    }
}
