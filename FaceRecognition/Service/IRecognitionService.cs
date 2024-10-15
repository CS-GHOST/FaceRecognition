using FaceRecognition.Entity;
using System.Drawing;

namespace FaceRecognition.Service
{
    /// <summary>
    /// 人脸识别接口
    /// </summary>
    interface IRecognitionService
    {
        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <param name="image">人脸图像</param>
        /// <param name="userInfo">注册用户信息</param>
        /// <returns></returns>
        FaceResult Register(Image image, UserInfo userInfo);

        /// <summary>
        /// 人脸识别
        /// </summary>
        /// <param name="image">人脸图像</param>
        /// <returns></returns>
        FaceResult Recognize(Image image);
    }
}
