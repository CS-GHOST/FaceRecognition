using FaceRecognition.Entity;
using FaceRecognition.Service.Entity;
using FaceRecognition.Utils;
using FaceRecognition.Utils.Sm4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Web.Script.Serialization;

namespace FaceRecognition.Service
{
    /// <summary>
    /// 人脸识别服务实现类
    /// </summary>
    internal class RecognizeService : IRecognitionService
    {
        #region 私有字段

        /// <summary>
        /// 接口成功码
        /// </summary>
        private const int SuccessCode = 200;
        /// <summary>
        /// 业务接口成功状态
        /// </summary>
        private const string SuccessStatus = "T";
        /// <summary>
        /// 是否加密标识
        /// </summary>
        private bool _encryptFlag = true;
        /// <summary>
        /// sm4加密实例
        /// </summary>
        private Sm4Crypto _sm4Crypto;
        /// <summary>
        /// 序列化服务实例
        /// </summary>
        private JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();
        /// <summary>
        /// 接口基地址
        /// </summary>
        private string _baseUrl = string.Empty;

        #endregion

        #region 公共方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config">配置信息</param>
        public RecognizeService(Config config)
        {
            _baseUrl = config.apiUrl;
            if (_encryptFlag && !string.IsNullOrWhiteSpace(config.apiKey))
            {
                _sm4Crypto = new Sm4Crypto();
                _sm4Crypto.HexString = true;
                _sm4Crypto.SecretKey = config.apiKey;
            }
        }

        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <param name="image">人脸图像</param>
        /// <param name="userInfo">注册用户信息</param>
        /// <returns></returns>
        public FaceResult Register(Image image, UserInfo userInfo)
        {
            FaceResult result = new FaceResult();

            RegisterRequest request = new RegisterRequest();
            request.idType = userInfo.idType;
            request.idNo = userInfo.idNo;
            request.name = userInfo.name;
            request.mobile = userInfo.mobile;
            request.cardNo = userInfo.cardNo;
            request.image = image;

            RegisterResponse response = Register(request);
            result.message = response?.msg;

            if (response != null && response.status == SuccessStatus)
            {
                result.code = "0";
                result.data = response.result;
            }
            else
            {
                result.code = "1";
            }

            return result;
        }

        /// <summary>
        /// 人脸识别
        /// </summary>
        /// <param name="image">人脸图像</param>
        /// <returns></returns>
        public FaceResult Recognize(Image image)
        {
            FaceResult result = new FaceResult();

            RecognizeRequest request = new RecognizeRequest();
            request.file = image;

            RecognizeResponse response = Recognize(request);
            result.message = response?.msg;
            if (response != null && response.status == SuccessStatus)
            {
                result.code = "0";
                result.data = response.getResult;
            }
            else
            {
                result.code = "1";
            }

            return result;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private RegisterResponse Register(RegisterRequest request)
        {
            RegisterResponse response = new RegisterResponse();

            try
            {
                string url = _baseUrl + "/tbs_001";
                string responseString = PostEntity(request, url, _encryptFlag);
                Response<RegisterResponse> resp = _javaScriptSerializer.Deserialize<Response<RegisterResponse>>(responseString);
                if (resp.code != SuccessCode)
                {
                    response.msg = resp.msg;
                }
                else
                {
                    response = resp.data;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Save("人脸注册异常：" + ex.Message);
                response = null;
            }

            return response;
        }

        /// <summary>
        /// 人脸识别
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private RecognizeResponse Recognize(RecognizeRequest request)
        {
            RecognizeResponse response = new RecognizeResponse();
            try
            {
                string url = _baseUrl + "/tbs_002";
                string responseString = PostEntity(request, url, _encryptFlag);
                Response<RecognizeResponse> resp = _javaScriptSerializer.Deserialize<Response<RecognizeResponse>>(responseString);
                if (resp.code != SuccessCode)
                {
                    response.msg = resp.msg;
                }
                else
                {
                    response = resp.data;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Save("人脸识别异常：" + ex.Message);
                response = null;
            }

            return response;
        }

        /// <summary>
        /// Post接口提交
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="url"></param>
        /// <param name="encryptStringProperty">是否加密字符串属性</param>
        /// <returns></returns>
        private string PostEntity<T>(T param, string url, bool encryptStringProperty)
        {
            PropertyInfo[] propertys = typeof(T).GetProperties();
            List<FormItemModel> list = new List<FormItemModel>();
            FormItemModel formItemModel;
            //遍历所有属性，获取参与签名属性，保存键值对
            foreach (PropertyInfo item in propertys)
            {
                formItemModel = new FormItemModel();
                object value = item.GetValue(param, null);
                if (typeof(DateTime) == item.PropertyType)
                {
                    formItemModel.Key = item.Name;
                    formItemModel.Value = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
                    list.Add(formItemModel);
                }
                else if (typeof(Enum).IsAssignableFrom(item.PropertyType))
                {
                    formItemModel.Key = item.Name;
                    formItemModel.Value = ((int)value).ToString();
                    list.Add(formItemModel);
                }
                else if (typeof(Image) == item.PropertyType)
                {
                    formItemModel.Key = item.Name;
                    formItemModel.FileContent = ImageTool.Image2Stream((Image)value);
                    formItemModel.FileName = ImageTool.GetStreamMd5Str(formItemModel.FileContent);

                    list.Add(formItemModel);
                }
                else
                {
                    string v = value.ToString();
                    if (encryptStringProperty)
                    {
                        v = _sm4Crypto?.EncryptECB(v);
                    }
                    formItemModel.Key = item.Name;
                    formItemModel.Value = v;
                    list.Add(formItemModel);
                }
            }

            return HttpTool.PostForm(url, list);
        }

        #endregion
    }
}
