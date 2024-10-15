using System.Runtime.InteropServices;
using System.Text;

namespace FaceRecognition.Utils
{
    /// <summary>
    /// ini配置文件工具类
    /// </summary>
    public class IniTool
    {
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Ansi)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 读取INI文件数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="section">节点名</param>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static string Read(string filePath, string section, string key)
        {
            StringBuilder temp = new StringBuilder(2048);
            int i = GetPrivateProfileString(section, key, "", temp, 2048, filePath);
            return temp.ToString();
        }

        /// <summary>
        /// 向INI文件写入数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="section">节点名</param>
        /// <param name="key">键名</param>
        /// <param name="value">值（字符串）</param>
        public static void Write(string filePath, string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, filePath);
        }
    }
}
