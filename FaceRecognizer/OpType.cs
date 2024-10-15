using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceRecognizer
{
    /// <summary>
    /// 操作类型
    /// </summary>
    internal enum OpType
    {
        /// <summary>
        /// 获取人脸图像
        /// </summary>
        /// FaceDetect
        FACE_DETECT = 1,
        /// <summary>
        /// 人脸注册
        /// </summary>
        FACE_REGISTER = 2,
        /// <summary>
        /// 人脸识别
        /// </summary>
        FACE_RECOGNIZE = 3,
    }
}
