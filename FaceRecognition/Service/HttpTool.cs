using FaceRecognition.Service.Entity;
using FaceRecognition.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace FaceRecognition.Service
{
    public class HttpTool
    {
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formItems"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string PostForm(string url, List<FormItemModel> formItems, int timeOut = 5 * 60 * 1000)
        {
            string result = string.Empty;
            try
            {
                #region 初始化请求对象
                //Https支持
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = timeOut;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.KeepAlive = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
                string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符
                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                #endregion

                #region 处理Form表单请求内容
                //请求流
                var postStream = new MemoryStream();

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
                request.ContentLength = postStream.Length;

                #endregion

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

                #region 返回
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        result = myStreamReader.ReadToEnd();
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            finally
            {
                LogHelper.Save(result);
            }

            return result;
        }
    }
}
