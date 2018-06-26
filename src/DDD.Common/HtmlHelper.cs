using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;

namespace DDD.Common
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
        /// <param name="str">Html编码后的字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string Decode(string str)
        {            
            if(str != null && !string.IsNullOrEmpty(str))
            {
                StringBuilder sb = new StringBuilder(str);

                sb = sb.Replace("&amp;", "&");
                sb = sb.Replace("&lt;", "<");
                sb = sb.Replace("&gt;", ">");
                sb = sb.Replace("&nbsp;", " ");
                sb = sb.Replace("&quot;", "'");

                str = sb.ToString().Trim();
            }         
            return str;
        }

        /// <summary>
        /// 将编码后的html字符串解码、去除空格空行、过滤A标签
        /// </summary>
        /// <param name="str">Html编码后的字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string TransformHtml(string str)
        {
            if (str != null && !string.IsNullOrEmpty(str))
            {
                str = Decode(str);
                str = RemoveWhiteSpace(str);
                str = RemoveSpecialTag(str);
                str = RemoveA(str);
                str = RemoveStyle(str);
            }
            return str;
        }

        /// <summary>
        /// 去除空格、空行
        /// </summary>
        /// <param name="htmlStr">html字符串</param>
        /// <returns></returns>
        public static string RemoveWhiteSpace(string htmlStr)
        {            
            if (htmlStr != null && !string.IsNullOrEmpty(htmlStr))
            {
                StringBuilder sb = new StringBuilder(htmlStr);

                sb = sb.Replace("\t", "");
                sb = sb.Replace("\r", "");
                sb = sb.Replace("\n", "");

                sb = sb.Replace("<br>", "");
                sb = sb.Replace("<br />", "");
                sb = sb.Replace("<p></p>", "");

                htmlStr = sb.ToString().Trim();
            }
            return htmlStr;
        }

        /// <summary>
        /// 过滤A标签
        /// </summary>
        /// <param name="htmlStr">html字符串</param>
        /// <returns></returns>
        public static string RemoveA(string htmlStr)
        {
            if(htmlStr != null && !string.IsNullOrEmpty(htmlStr))
            {
                string aBeginPattern = @"<a\s*[^>]*>";
                string aEndPatten = @"</a>";                     
                htmlStr = Regex.Replace(htmlStr, aBeginPattern, "", RegexOptions.IgnoreCase);
                htmlStr = Regex.Replace(htmlStr, aEndPatten, "", RegexOptions.IgnoreCase);
            }            
            return htmlStr;
        }

        /// <summary>
        /// 过滤style样式
        /// </summary>
        /// <param name="htmlStr"></param>
        /// <returns></returns>
        public static string RemoveStyle(string htmlStr)
        {
            if (htmlStr != null && !string.IsNullOrEmpty(htmlStr))
            {
                string stylePattern = @"\s*style\s*=\s*('|"")[^'|^""]*('|"")";
                htmlStr = Regex.Replace(htmlStr, stylePattern, "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            return htmlStr;
        }

        /// <summary>
        /// 去除特殊标签
        /// </summary>
        /// <param name="htmlStr">html字符串</param>
        /// <returns></returns>
        public static string RemoveSpecialTag(string htmlStr)
        {
            if (htmlStr != null && !string.IsNullOrEmpty(htmlStr))
            {
                StringBuilder sb = new StringBuilder(htmlStr);

                sb = sb.Replace("\\", "");
                sb = sb.Replace("''", "'");

                htmlStr = sb.ToString().Trim();
            }
            return htmlStr;
        }

        /// <summary>
        /// html字符串转为纯文本
        /// </summary>
        /// <param name="htmlStr">html字符串</param>
        /// <returns></returns>
        public static string HtmlToText(string htmlStr)
        {
            if(htmlStr != null && !string.IsNullOrEmpty(htmlStr))
            {
                string regEx_style = "<style[^>]*?>[\\s\\S]*?<\\/style>";       //定义style的正则表达式   
                string regEx_script = "<script[^>]*?>[\\s\\S]*?<\\/script>";    //定义script的正则表达式   
                string regEx_html = "<[^>]+>";                                  //定义HTML标签的正则表达式                
                htmlStr = Regex.Replace(htmlStr, regEx_style, "");              //删除css  
                htmlStr = Regex.Replace(htmlStr, regEx_script, "");             //删除js  
                htmlStr = Regex.Replace(htmlStr, regEx_html, "");               //删除html标记  
            }
            return htmlStr;
        }

        /// <summary>
        /// 获取页面的链接正则
        /// </summary>
        public string GetHref(string HtmlCode)
        {
            string MatchVale = "";
            string Reg = @"(h|H)(r|R)(e|E)(f|F) *= *('|"")?((\w|\\|\/|\.|:|-|_)+)[\S]*";
            foreach (Match m in Regex.Matches(HtmlCode, Reg))
            {
                MatchVale += (m.Value).ToLower().Replace("href=", "").Trim() + "|";
            }
            return MatchVale;
        }

        /// <summary>
        /// 匹配页面的图片地址
        /// </summary>
        /// <param name="imgHttp">要补充的http://路径信息</param>
        public static string GetImgSrc(string HtmlCode, string imgHttp)
        {
            string MatchVale = "";
            string Reg = @"<img.+?>";
            foreach (Match m in Regex.Matches(HtmlCode.ToLower(), Reg))
            {
                MatchVale += GetImg((m.Value).ToLower().Trim(), imgHttp) + "|";
            }

            return MatchVale;
        }

        /// <summary>
        /// 匹配<img src="" />中的图片路径实际链接
        /// </summary>
        /// <param name="ImgString"><img src="" />字符串</param>
        public static string GetImg(string ImgString, string imgHttp)
        {
            string MatchVale = "";
            string Reg = @"src=.+\.(bmp|jpg|gif|png|)";
            foreach (Match m in Regex.Matches(ImgString.ToLower(), Reg))
            {
                MatchVale += (m.Value).ToLower().Trim().Replace("src=", "");
            }
            if ( MatchVale.IndexOf(".com") != -1 || MatchVale.IndexOf(".cn") != -1 || MatchVale.IndexOf(".net") != -1 || MatchVale.IndexOf(".org") != -1)
                return (MatchVale);
            else
                return (imgHttp + MatchVale);
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
            bodyStr = HtmlHelper.TransformHtml(bodyStr);
            var bodySB = new StringBuilder(); 

            headStr.Append("<meta http-equiv='content-type' content='text/html; charset=utf-8' />");
            headStr.Append("<meta name='viewport' content='initial-scale=1.0,maximum-scale=1.0, user-scalable=no' />");
            headStr.Append("<link rel='shortcut icon' type='image/x-icon' href='http://image.chsgw.com/favicon.ico' media='screen' />");
            cssStr.Append("<link rel='stylesheet' type='text/css' href='http://m.chsgw.com/app/css/global.css' />");
            cssStr.Append("<link rel='stylesheet' type='text/css' href='http://m.chsgw.com/app/css/news-detail.css' />");
            jsStr.Append("<script type='text/javascript' src='http://m.chsgw.com/app/js/pub.js'></script>");

            bodySB.Append(string.Format("<div class='news_main'><div class='news-body'><div class='news-body_main'>{0}</div></div></div>", bodyStr));
            var returnStr = BuildHtml(title, cssStr.ToString(), jsStr.ToString(), headStr.ToString(), bodySB.ToString());
            return returnStr;
        }
    }
}
