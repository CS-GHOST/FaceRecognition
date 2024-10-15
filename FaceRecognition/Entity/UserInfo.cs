namespace FaceRecognition.Entity
{
    /// <summary>
    /// 注册用户信息实体
    /// </summary>
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

        /// <summary>
        /// 门诊号
        /// </summary>
        public string cardNo { get; set; }
    }
}
