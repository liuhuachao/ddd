using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;

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
        public static string CreateSignForJsSdk(Dictionary<string,string> dic)
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
                return WeixinHelper.CreateSign(args);
            }
            set
            {
                _signature = value;
            }
        }
    }

}
