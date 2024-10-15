using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceRecognizer
{
    public class Invoke
    {
        public static FaceResult FaceDetect(Config config)
        {
            FaceResult result = new FaceResult();

            try
            {
                FaceForm faceForm = new FaceForm(config);
                faceForm.OpType = OpType.FACE_DETECT;
                faceForm.ShowDialog();
                result = faceForm.FaceResult;
            }
            catch (Exception ex)
            {
                result.code = "2";
                result.message = "处理异常：" + ex.Message;
            }

            return result;
        }

        public static FaceResult FaceRegister(Config config, UserInfo userInfo)
        {
            FaceResult result = new FaceResult();

            try
            {
                FaceForm faceForm = new FaceForm(config);
                faceForm.OpType = OpType.FACE_REGISTER;
                faceForm.RegisterUserInfo = userInfo;
                faceForm.ShowDialog();
                result = faceForm.FaceResult;
            }
            catch (Exception ex)
            {
                result.code = "2";
                result.message = "处理异常：" + ex.Message;
            }

            return result;
        }

        public static FaceResult FaceRecognize(Config config)
        {
            FaceResult result = new FaceResult();

            try
            {
                FaceForm faceForm = new FaceForm(config);
                faceForm.OpType = OpType.FACE_RECOGNIZE;
                faceForm.ShowDialog();
                result = faceForm.FaceResult;
            }
            catch (Exception ex)
            {
                result.code = "2";
                result.message = "处理异常：" + ex.Message;
            }

            return result;
        }
    }
}
