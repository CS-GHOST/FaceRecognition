using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FaceRecognizer
{
    public static class Utils
    {
        public static Bitmap DeepClone(this Bitmap bitmap)
        {
            Bitmap dstBitmap = null;
            using (MemoryStream mStream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(mStream, bitmap);
                mStream.Seek(0, SeekOrigin.Begin);
                dstBitmap = (Bitmap)bf.Deserialize(mStream);
                mStream.Close();
            }
            return dstBitmap;
        }

        public static Bitmap BitmapDeepClone(Bitmap bitmap)
        {
            Bitmap dstBitmap = null;
            using (MemoryStream mStream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(mStream, bitmap);
                mStream.Seek(0, SeekOrigin.Begin);//指定当前流的位置为流的开头。
                dstBitmap = (Bitmap)bf.Deserialize(mStream);
                mStream.Close();
            }
            return dstBitmap;

        }
    }
}
