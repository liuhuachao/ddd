using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace WebApi.Common
{
    /// <summary>
    /// Html 帮助类
    /// </summary>
    public class HtmlHelper
    {
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Encode(string str)
        {
            return System.Web.HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="encodeStr">Html编码后的字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string Decode(string encodeStr)
        {            
            var decodeStr = encodeStr;
            if(encodeStr != null && !string.IsNullOrEmpty(decodeStr))
            {
                decodeStr = decodeStr.Replace("&amp;", "&");
                decodeStr = decodeStr.Replace("&lt;", "<");
                decodeStr = decodeStr.Replace("&gt;", ">");
                decodeStr = decodeStr.Replace("&nbsp;", " ");
                decodeStr = decodeStr.Replace("&quot;", "'");

                decodeStr = decodeStr.Replace("\r", "");
                decodeStr = decodeStr.Replace("\t", "");
                decodeStr = decodeStr.Replace("\n", "");

                decodeStr = decodeStr.Replace("\\", "");
                decodeStr = decodeStr.Replace("''", "'");
                decodeStr = decodeStr.Replace("<br>", "");
                decodeStr = decodeStr.Replace("<br />", "");
                decodeStr = decodeStr.Replace("<p></p>", "");
            }         
            return decodeStr;
        }

        /// <summary>
        /// 构建Html
        /// </summary>
        /// <param name="headStr"></param>
        /// <param name="bodyStr"></param>
        /// <returns></returns>
        public static string BuildHtml(string headStr,string bodyStr)
        {
            StringBuilder sbHtml = new StringBuilder("<!DOCTYPE html>");
            sbHtml.Append("<html>");
            sbHtml.Append(string.Format("<head>{0}</head>",headStr));
            sbHtml.Append(string.Format("<body>{0}</body>",bodyStr));            
            sbHtml.Append("</html>");
            return sbHtml.ToString();
        }
        public static string BuildHtml(string title, string cssStr,string jsStr, string headStr, string bodyStr)
        {
            StringBuilder sbHtml = new StringBuilder("<!DOCTYPE html>");
            sbHtml.Append("<html>");
            sbHtml.Append("<head>");
            sbHtml.Append(string.Format("<title>{0}</title>",title));
            sbHtml.Append(headStr);
            sbHtml.Append(cssStr);
            sbHtml.Append("</head>");
            sbHtml.Append("<body>");
            sbHtml.Append(bodyStr);
            sbHtml.Append(jsStr);
            sbHtml.Append("</body>");
            sbHtml.Append("</html>");
            return sbHtml.ToString();
        }

        /// <summary>
        /// 构建资讯详情页
        /// </summary>
        public static string BuildNewsDetailHtml(string bodyStr)
        {
            var title = "资讯详情页-尊贵赛鸽";
            var cssStr = new StringBuilder();
            var jsStr = new StringBuilder();
            var headStr = new StringBuilder();
            bodyStr = HtmlHelper.Decode(bodyStr);
            var bodySB = new StringBuilder(); 
            var openAppStr = new StringBuilder();

            openAppStr.Append("<div class='news_main'>");
            openAppStr.Append("<div class='news-banner-container'>");
            openAppStr.Append("<div class='news-banner-left'>");
            openAppStr.Append("<div class='news-banner-logo'><img src='http://m.chsgw.com/app/img/logo.png' /></div>");
            openAppStr.Append("<div class='news-banner-text'>");
            openAppStr.Append("<p>尊贵赛鸽</p>");
            openAppStr.Append("<p>有态度、有温度、有深度</p>");
            openAppStr.Append("</div>");
            openAppStr.Append("</div>");
            openAppStr.Append("<div class='news-banner-right'>");
            openAppStr.Append("<a href='#'>打开</a>");
            openAppStr.Append("</div>");
            openAppStr.Append("</div>");

            headStr.Append("<meta http-equiv='content-type' content='text/html; charset=utf-8' />");
            headStr.Append("<meta name='viewport' content='initial-scale=1.0,maximum-scale=1.0, user-scalable=no' />");
            headStr.Append("<link rel='shortcut icon' type='image/x-icon' href='http://image.chsgw.com/favicon.ico' media='screen' />");
            cssStr.Append("<link rel='stylesheet' type='text/css' href='http://m.chsgw.com/app/css/global.css' />");
            cssStr.Append("<link rel='stylesheet' type='text/css' href='http://m.chsgw.com/app/css/news-detail.css' />");
            jsStr.Append("<script type='text/javascript' src='http://m.chsgw.com/app/js/pub.js'></script>");

            //bodySB.Append(openAppStr);
            bodySB.Append(string.Format("<div class='news-body'><div class='news-body_main'>{0}</div></div></div>", bodyStr));
            var returnStr = BuildHtml(title, cssStr.ToString(), jsStr.ToString(), headStr.ToString(), bodySB.ToString());
            return returnStr;
        }
    }
}
