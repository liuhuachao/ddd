using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;

namespace DDD.Common
{
    public class JSSDKHelper
    {
        private static string[] strArray = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

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
            string signature = string.Join("", args);
            signature = Sha1(signature);
            signature = signature.ToLower();
            return signature;
        }

        /// <summary>
        /// 生成微信签名signature,js-sdk
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string CreateSignCommon(Dictionary<string,string> dic)
        {
            dic = dic.OrderBy(d => d.Key).ToDictionary(k => k.Key,v => v.Value) ;
            IList<string> list = new List<string>();            
            foreach (var item in dic)
            {
                list.Add(item.Key.ToLower() + "=" + item.Value);
            }
            string signature = string.Join("&",list);
            signature = Sha1(signature);
            signature = signature.ToLower();
            return signature;
        }

        public static string CreateSign(string ticket, string noncestr, string timestamp, string url)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("jsapi_ticket", ticket);
            dic.Add("noncestr", noncestr);
            dic.Add("timestamp", timestamp);
            dic.Add("url", url);
            var signature = CreateSignCommon(dic);
            return signature;
        }

        /// <summary>
        /// Sha1加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Sha1(string text)
        {
            byte[] cleanBytes = Encoding.Default.GetBytes(text);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="args">输入字符串</param>
        /// <param name="length">返回字符串的长度</param>
        /// <returns></returns>
        public static string GetNonceStr(string[] args,int length)
        {            
            Random r = new Random();
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(args[r.Next(args.Length - 1)]);
            }
            return sb.ToString();
        }
        public static string GetNonceStr(int length)
        {
            var args = strArray;
            return GetNonceStr(args,length);
        }
    }

    /// <summary>
    /// 微信分享js-sdk
    /// </summary>
    public class WeixinShare
    {
        private string _signature;

        [DisplayName("appId")]
        public string AppId { get; set; }
        [DisplayName("noncestr")]
        public string NonceStr { get; set; }
        [DisplayName("jsapi_ticket")]
        public string Ticket { get; set; }
        [DisplayName("timestamp")]
        public string Timestamp { get; set; }
        [DisplayName("url")]
        public string Url { get; set; }
        [DisplayName("signature")]
        public string Signature
        {
            get
            {
                string[] args = new string[] { this.NonceStr,this.Ticket,this.Timestamp,this.Timestamp,this.Url};
                return JSSDKHelper.CreateSign(args);
            }
            set
            {
                _signature = value;
            }
        }
    }

}
