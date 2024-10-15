using System.Text;

namespace FaceRecognition.Utils.Sm4
{
    public class Sm4Crypto
    {
        public string SecretKey = "";
        public string Iv = "";
        public bool HexString = false;

        #region ECB模式加密

        /// <summary>
        /// ECB模式加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string EncryptECB(string plainText)
        {
            SM4Context ctx = new SM4Context();
            ctx.isPadding = true;
            ctx.mode = SM4.SM4_ENCRYPT;

            byte[] keyBytes;
            if (HexString)
            {
                keyBytes = Hex.Decode(SecretKey);
            }
            else
            {
                keyBytes = Encoding.Default.GetBytes(SecretKey);
            }

            SM4 sm4 = new SM4();
            sm4.sm4_setkey_enc(ctx, keyBytes);
            byte[] encrypted = sm4.sm4_crypt_ecb(ctx, Encoding.UTF8.GetBytes(plainText));

            return Hex.Encode(encrypted);
        }

        #endregion

        #region ECB模式解密

        /// <summary>
        /// ECB模式解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string DecryptECB(string cipherText)
        {
            SM4Context ctx = new SM4Context();
            ctx.isPadding = true;
            ctx.mode = SM4.SM4_DECRYPT;

            byte[] keyBytes;
            if (HexString)
            {
                keyBytes = Hex.Decode(SecretKey);
            }
            else
            {
                keyBytes = Encoding.Default.GetBytes(SecretKey);
            }

            SM4 sm4 = new SM4();
            sm4.sm4_setkey_dec(ctx, keyBytes);
            byte[] decrypted = sm4.sm4_crypt_ecb(ctx, Hex.Decode(cipherText));
            return Encoding.Default.GetString(decrypted);
        }

        #endregion
    }
}
