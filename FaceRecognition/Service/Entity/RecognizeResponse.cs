using System.Collections.Generic;

namespace FaceRecognition.Service.Entity
{
    /// <summary>
    /// 人脸识别查证响应实体
    /// </summary>
    public class RecognizeResponse : BaseData
    {
        /// <summary>
        /// 结果集
        /// </summary>
        public List<RecognizeResult> getResult { get; set; }
    }

    public class RecognizeResult
    {
        /// <summary>
        /// 图片ID
        /// </summary>
        public string imageId { get; set; }

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
        /// 手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 人脸识别置信度
        /// </summary>
        public string similary { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string createTime { get; set; }

        /// <summary>
        /// 源图像
        /// </summary>
        public string originalImage { get; set; }

        /// <summary>
        /// 源图像类型
        /// </summary>
        public string originalImageType { get; set; }
    }
}
