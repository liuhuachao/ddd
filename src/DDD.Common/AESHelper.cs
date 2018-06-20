using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Common
{
    /// <summary>
    /// AES加密/解密类
    /// </summary>
    public class AESHelper
    {
        private const string KEY = "0e7449b7d5d0495a9869eeb1a8043c9d"; 
        public AESHelper()
        {
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string inputStr, string key)
        {
            var encryptKey = Encoding.UTF8.GetBytes(key);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(encryptKey, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor,
                            CryptoStreamMode.Write))

                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(inputStr);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result,
                            iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string AESEncrypt(string input)
        {
            return AESEncrypt(input, KEY);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string AESDecrypt(string inputStr, string key)
        {
            var fullCipher = Convert.FromBase64String(inputStr);
            byte[] iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
            var decryptKey = Encoding.UTF8.GetBytes(key);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(decryptKey, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        public static string AESDecrypt(string input)
        {
            return AESDecrypt(input, KEY);
        }

    }
}
