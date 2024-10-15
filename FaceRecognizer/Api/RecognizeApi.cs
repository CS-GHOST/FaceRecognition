using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FaceRecognizer.Api
{
    internal class RecognizeApi
    {
        static string url = "https://smart.hsop.site:18120/tbs/tbs_001"; //http://127.0.0.1:60000";// 
        public static bool RecognizeS(Bitmap bitmap)
        {
            Thread.Sleep(2000);
            Console.WriteLine(DateTime.Now + "Recognize");
            return true;
        }
        public static async Task<RecognizeResponse> Recognize(RecognizeRequest request)
        {
            Task<RecognizeResponse> task = new Task<RecognizeResponse>(() =>
            {
                string res = HttpTool.HttpPost(request, url);
                RecognizeResponse response = new JavaScriptSerializer().Deserialize<RecognizeResponse>(res);
                return response;
            });
            task.Start();
            await task;
            return task.Result;
        }

        public static async Task<RegisterResponse> Register(RegisterRequest request)
        {
            Task<RegisterResponse> task = new Task<RegisterResponse>(() =>
            {
                string res = HttpTool.HttpPost(request, url);
                RegisterResponse response = new JavaScriptSerializer().Deserialize<RegisterResponse>(res);
                return response;
            });
            task.Start();
            await task;
            return task.Result;
        }



        //public static string BitmapToBase64(Image bitmap)
        //{
        //    MemoryStream ms1 = new MemoryStream();
        //    bitmap.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    byte[] arr1 = new byte[ms1.Length];
        //    ms1.Position = 0;
        //    ms1.Read(arr1, 0, (int)ms1.Length);
        //    ms1.Close();
        //    return Convert.ToBase64String(arr1);
        //}
    }
}
