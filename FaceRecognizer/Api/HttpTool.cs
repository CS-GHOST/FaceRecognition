using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace FaceRecognizer.Api
{
    public class HttpTool
    {
        /// <summary>
        /// 上传文件接口
        /// </summary>
        /// <param name="dictParam">上传参数</param>
        /// <param name="fileUrl">文件的地址</param>
        /// <param name="url">请求接口地址</param>
        /// <param name="encoding">字符编码格式</param>
        /// <param name="keyName">文件参数的参数名称</param>
        /// <returns></returns>
        public static string UploadRequest(Dictionary<string, object> dictParam, string fileUrl, Encoding encoding, string url, string keyName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebHeaderCollection header = request.Headers;
            request.Method = "post";

            //读取文件信息
            FileStream fileStream = new FileStream(fileUrl, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();

            byte[] UpdateFile = bytes;//转换为二进制
            if (UpdateFile.Length == 0)
            {
                return "上传文件无效";
            }
            string Boundary = "--WebKitFormBoundary39B5a5e2FWoGbphs";
            //构造POST请求体
            StringBuilder PostContent = new StringBuilder("");

            //获取文件名称
            string filename = "";
            filename = Path.GetFileName(fileUrl);

            //组成普通参数信息
            foreach (KeyValuePair<string, object> item in dictParam)
            {
                PostContent.Append("--" + Boundary + "\r\n")
                           .Append("Content-Disposition: form-data; name=\"" + item.Key + "\"" + "\r\n\r\n" + (string)item.Value + "\r\n");
            }
            byte[] PostContentByte = encoding.GetBytes(PostContent.ToString());

            //处理文件参数信息
            StringBuilder FileContent = new StringBuilder();
            FileContent.Append("--" + Boundary + "\r\n")
                    .Append("Content-Disposition:form-data; name=\"" + keyName + "\";filename=\"" + filename + "\"" + "\r\n\r\n");
            byte[] FileContentByte = encoding.GetBytes(FileContent.ToString());
            request.ContentType = "multipart/form-data;boundary=" + Boundary;
            byte[] ContentEnd = encoding.GetBytes("\r\n--" + Boundary + "--\r\n");//请求体末尾，后面会用到
            //定义请求流
            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(PostContentByte, 0, PostContentByte.Length);//写入参数
            myRequestStream.Write(FileContentByte, 0, FileContentByte.Length);//写入文件信息
            myRequestStream.Write(UpdateFile, 0, UpdateFile.Length);//文件写入请求流中
            myRequestStream.Write(ContentEnd, 0, ContentEnd.Length);//写入结尾   
            myRequestStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encodingth = response.ContentEncoding;
            if (encodingth == null || encodingth.Length < 1)
            {
                encodingth = "GBK"; //默认编码,根据需求自己指定 
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encodingth));
            string retString = reader.ReadToEnd();
            return retString;
        }

        public static string HttpPost<T>(T param, string url)
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
                    string fileName = @"C:\EF4C6A66F4A13DA5F6DA4CD138C95784D7C023131BF0478E12937D418468064F.bmp";
                    FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                    formItemModel.FileContent = stream;// Image2Stream((Image)value);
                    formItemModel.FileName = GetStreamMd5Str(formItemModel.FileContent);

                    list.Add(formItemModel);
                }
                else
                {
                    formItemModel.Key = item.Name;
                    formItemModel.Value = value.ToString();
                    list.Add(formItemModel);
                }
            }

            return PostForm(url, list);
        }

        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formItems"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string PostForm(string url, List<FormItemModel> formItems, int timeOut = 5 * 60 * 1000)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                #region 初始化请求对象
                request.Method = "POST";
                request.Timeout = timeOut;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.KeepAlive = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
                #endregion

                string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符
                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                //请求流
                var postStream = new MemoryStream();
                #region 处理Form表单请求内容
                //是否用Form上传文件
                var formUploadFile = formItems != null && formItems.Count > 0;
                if (formUploadFile)
                {
                    //文件数据模板
                    string fileFormdataTemplate =
                        "\r\n--" + boundary +
                        "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                        "\r\nContent-Type: image/png" +
                        "\r\n\r\n";
                    //文本数据模板
                    string dataFormdataTemplate =
                        "\r\n--" + boundary +
                        "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                        "\r\n\r\n{1}";
                    foreach (var item in formItems)
                    {
                        string formdata = null;
                        if (item.IsFile)
                        {
                            //上传文件
                            formdata = string.Format(
                                fileFormdataTemplate,
                                item.Key, //表单键
                                item.FileName);
                        }
                        else
                        {
                            //上传文本
                            formdata = string.Format(
                                dataFormdataTemplate,
                                item.Key,
                                item.Value);
                        }

                        //统一处理
                        byte[] formdataBytes = null;
                        //第一行不需要换行
                        if (postStream.Length == 0)
                        {
                            formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                        }
                        else
                        {
                            formdataBytes = Encoding.UTF8.GetBytes(formdata);
                        }
                        postStream.Write(formdataBytes, 0, formdataBytes.Length);

                        //写入文件内容
                        if (item.FileContent != null && item.FileContent.Length > 0)
                        {
                            using (var stream = item.FileContent)
                            {
                                byte[] buffer = new byte[1024];
                                int bytesRead = 0;
                                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    postStream.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }
                    //结尾
                    var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                    postStream.Write(footer, 0, footer.Length);
                }
                else
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }
                #endregion

                request.ContentLength = postStream.Length;

                #region 输入二进制流
                if (postStream != null)
                {
                    postStream.Position = 0;
                    //直接写入流
                    Stream requestStream = request.GetRequestStream();

                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }
                    postStream.Close();//关闭文件访问
                }
                #endregion

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        string retString = myStreamReader.ReadToEnd();
                        return retString;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }


        private static Stream Image2Stream(Image image)
        {
            Stream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Bmp);
            return stream;
        }
        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="path">文件全名</param>
        /// <returns></returns>
        public static string GetStreamMd5Str(Stream stream)
        {
            try
            {
                int bufferSize = 1024 * 16;//自定义缓冲区大小16K 
                byte[] buffer = new byte[bufferSize];
                Stream inputStream = stream;
                HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
                int readLength = 0;//每次读取长度 
                var output = new byte[bufferSize];
                while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    //计算MD5 
                    hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
                }
                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0) 
                hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
                string md5 = BitConverter.ToString(hashAlgorithm.Hash).ToUpper();
                hashAlgorithm.Clear();
                inputStream.Seek(0, SeekOrigin.Begin);
                return md5.Replace("-", "");
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
