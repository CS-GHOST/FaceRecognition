using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;

namespace FaceRecognition.Utils
{
    public class ImageTool
    {
        /// <summary>
        /// 人脸检测引擎
        /// </summary>
        private static readonly CascadeClassifier FaceDetect = new CascadeClassifier(@"haarcascade\haarcascade_frontalface_default.xml");

        /// <summary>
        /// Bitmap转Mat
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Mat Bitmap2Mat(Bitmap bitmap)
        {
            Image<Bgr, byte> imageCv = new Image<Bgr, byte>(bitmap);
            Mat mat = imageCv.Mat;
            return mat;
        }

        /// <summary>
        /// 获取最大宽度矩形框
        /// </summary>
        /// <param name="rectangles"></param>
        /// <returns></returns>
        public static Rectangle GetMaxRectangle(Rectangle[] rectangles)
        {
            if (rectangles == null || rectangles.Length == 0)
            {
                return Rectangle.Empty;
            }
            Rectangle maxRectangle = rectangles[0];
            for (int i = 1; i < rectangles.Length; i++)
            {
                if (rectangles[i].Width > maxRectangle.Width)
                {
                    maxRectangle = rectangles[i];
                }
            }

            return maxRectangle;
        }

        /// <summary>
        /// 获取图片中最大人脸框
        /// </summary>
        /// <param name="bitmap">bitmap图片</param>
        /// <param name="faceDetect">人脸检测引擎</param>
        /// <returns></returns>
        public static Rectangle GetMaxFaceRect(Bitmap bitmap)
        {
            Rectangle result = Rectangle.Empty;

            try
            {
                Mat mat = Bitmap2Mat(bitmap);
                Rectangle[] rects = FaceDetect.DetectMultiScale(mat);
                return GetMaxRectangle(rects);
            }
            catch (Exception ex)
            {
                LogHelper.Save("申请内存失败：" + ex.Message);
                GC.Collect();
            }

            return result;
        }

        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <param name="originImage">原图片</param>
        /// <param name="region">裁剪的方形区域</param>
        /// <returns>裁剪后图片</returns>
        public static Bitmap CropImage(Bitmap originImage, Rectangle region)
        {
            Bitmap result = new Bitmap(region.Width, region.Height);
            Graphics graphics = Graphics.FromImage(result);
            graphics.DrawImage(originImage, new Rectangle(0, 0, region.Width, region.Height), region, GraphicsUnit.Pixel);
            return result;
        }

        /// <summary>
        /// Image转Stream
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Stream Image2Stream(Image image)
        {
            Stream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Bmp);
            return stream;
        }

        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="stream">文件流</param>
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
                stream.Seek(0, SeekOrigin.Begin);
                hashAlgorithm.Clear();
                return md5.Replace("-", "");
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
