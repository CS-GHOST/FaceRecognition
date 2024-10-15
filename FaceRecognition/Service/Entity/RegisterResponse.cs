namespace FaceRecognition.Service.Entity
{
    /// <summary>
    /// 人脸识别建档响应实体
    /// </summary>
    public class RegisterResponse : BaseData
    {
        /// <summary>
        /// 结果
        /// </summary>
        public RegisterResult result { get; set; }
    }

    public class RegisterResult
    {
        /// <summary>
        /// 图像ID
        /// </summary>
        public string imageId { get; set; }
    }
}
