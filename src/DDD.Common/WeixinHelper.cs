using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Common
{
    public class WeixinHelper
    {
        /// <summary>
        /// 验证微信签名
        /// </summary>
        public bool CheckSignature(string[] args, string signature)
        {
            var tmpStr = CreateSign(args);
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 生成微信签名
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string CreateSign(string[] args)
        {
            Array.Sort(args);
            string signature = string.Join("&", args);
            signature = Sha1(signature);
            signature = signature.ToLower();
            return signature;
        }

        /// <summary>
        /// Sha1加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Sha1(string text)
        {
            byte[] cleanBytes = Encoding.UTF8.GetBytes(text);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

    }
}
