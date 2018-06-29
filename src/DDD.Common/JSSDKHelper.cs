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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetAccessToken(string appId,string appSecret,string grantType = "client_credential")
        {
            string send_url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}" , grantType,appId,appSecret);
            string result = HttpHelper.HttpGet(send_url);
            if (result.Contains("errcode"))
            {
                return null;
            }
            try
            {
                var dic = JsonHelper.JsonToDic(result);
                if (dic.Count > 0)
                {
                    return dic;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// 取得公众号jsapi_ticket
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetTicket(string access_token)
        {
            string send_url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + access_token + "&type=jsapi";
            string result = HttpHelper.HttpGet(send_url);
            try
            {
                var dic = JsonHelper.JsonToDic(result);
                if (dic.Count > 0)
                {
                    return dic;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

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
        public static string GetNonceStr()
        {
            var nonceStr = AESHelper.AESEncrypt(Guid.NewGuid().ToString());
            return nonceStr;
        }
    }

    /// <summary>
    /// 微信分享js-sdk
    /// </summary>
    public class WeixinShare
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string NonceStr { get; set; }
        public string Ticket { get; set; }
        public string Timestamp { get; set; }
        public string Url { get; set; }
        public string Signature { get; set; }
    }

}
