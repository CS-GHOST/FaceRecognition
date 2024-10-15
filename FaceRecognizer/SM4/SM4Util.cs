using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace FaceRecognizer.SM4
{
    public class SM4Util
    {
        public static string EncryptECB(string inputStr, string keyStr, string ivStr)
        {
            byte[] plaintext = Encoding.UTF8.GetBytes(inputStr);
            byte[] keyBytes = StrToHexByte(keyStr);
            byte[] iv = Encoding.UTF8.GetBytes(ivStr);
            // SM4/ECB加密
            KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
            //ParametersWithIV keyParamWithIv = new ParametersWithIV(key, iv);
            ParametersWithIV keyParamWithIv = new ParametersWithIV(key, iv);

            //IBufferedCipher inCipher = CipherUtilities.GetCipher("SM4/CBC/PKCS7Padding");
            IBufferedCipher inCipher = CipherUtilities.GetCipher("SM4/ECB/PKCS7Padding");
            //inCipher.Init(true, keyParamWithIv);
            inCipher.Init(true, keyParamWithIv.Parameters);
            byte[] cipher = inCipher.DoFinal(plaintext);
            //Console.WriteLine("加密后的密文(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
            return ByteToHexStr(cipher); //BitConverter.ToString(cipher).Replace("-", string.Empty);
        }

        public static string DecryptECB(string inputStr, string keyStr)
        {
            byte[] plaintext = Encoding.UTF8.GetBytes(inputStr);
            byte[] keyBytes = StrToHexByte(keyStr);
            IBufferedCipher inCipher = CipherUtilities.GetCipher("SM4/ECB/PKCS7Padding");
            // SM4/ECB加密
            KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
            // SM4/ECB解密
            inCipher.Init(false, key);
            byte[] cipher = inCipher.DoFinal(plaintext);
            byte[] bin = inCipher.DoFinal(cipher);
            string ans = Encoding.UTF8.GetString(bin);
            Console.WriteLine("解密后的密文(hex): {0}", ByteToHexStr(cipher).Replace("-", string.Empty));
            return ans;
        }


        /// <summary>
        /// 将16进制的字符串转为byte[]
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] StrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary> 
        /// 字节数组转16进制字符串 
        /// </summary> 
        /// <param name="bytes"></param> 
        /// <returns></returns> 
        public static string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
    }
}
