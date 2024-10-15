using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace FaceRecognition.Utils
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="context">日志内容</param>
        public static void Save(string context)
        {
            DateTime dtNow = DateTime.Now;
            string function = GetFunName(3);
            string logData = string.Format("[{0}] [{1}] [{2}]", dtNow.ToString("HH:mm:ss.fff"), function, context);
            string savePath = AppDomain.CurrentDomain.BaseDirectory + "\\" + "FaceRecognitionLogs" + "\\" + dtNow.Year + "\\" + dtNow.Month + "\\" + dtNow.Day;
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string fileName = dtNow.ToString("HH_mm_ss") + ".log";
            string oldFileName = GetLastFile(savePath, dtNow);
            if (!string.IsNullOrEmpty(oldFileName))
            {
                string lastFile = savePath + "\\" + oldFileName;
                if (File.Exists(lastFile))
                {
                    FileInfo fi = new FileInfo(lastFile);
                    if (oldFileName.StartsWith(dtNow.ToString("HH")) && fi.Length < 1024 * 1024)
                    {
                        fileName = oldFileName;
                    }
                }
            }
            AddText(savePath + "\\" + fileName, logData + "\r\n");
        }

        /// <summary>
        /// 往指定的路径（包含文件名）追加指定的数据
        /// </summary>
        /// <param name="savePath">路径（包含文件名）</param>
        /// <param name="data">需要写入的数据</param>
        private static void AddText(string savePath, string data)
        {
            try
            {
                using (FileStream fs = new FileStream(savePath, FileMode.Append))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.Write(data);
                        sw.Flush();
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获取堆栈执行的方法
        /// </summary>
        /// <param name="num">第几个方法</param>
        /// <returns></returns>
        private static string GetFunName(int num)
        {
            string funName = string.Empty;
            StackTrace st = new StackTrace();
            MethodBase mb = st.GetFrame(num).GetMethod();
            string name = mb.Name;
            string[] full = mb.DeclaringType.FullName.Split('.');
            funName = full[full.Length - 1] + "." + name;
            return funName;
        }

        /// <summary>
        /// 获取最后一个文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static string GetLastFile(string path, DateTime dt)
        {
            string file = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    FileInfo[] fil = dir.GetFiles();
                    List<string> list = new List<string>();//同一小时的文件
                    foreach (FileInfo f in fil)
                    {
                        if (f.Name.StartsWith(dt.ToString("HH")) && f.Length < 1024 * 1024)
                        {
                            list.Add(f.Name);
                        }
                    }
                    if (list.Count == 1)
                    {
                        file = list[0];
                    }
                    else if (list.Count > 1)
                    {
                        list.Sort();
                        file = list[list.Count - 1];
                    }
                }
            }
            return file;
        }
    }
}
