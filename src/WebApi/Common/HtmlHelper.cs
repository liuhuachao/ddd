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
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Decode(string str)
        {
            return System.Web.HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 加载CSS样式文件
        /// </summary>
        public static string LinkCss(string cssPath)
        {
            return @"<link rel=""stylesheet"" type=""text/css"" href=""" + cssPath + @""" />" + "\r\n";
        }

        /// <summary>
        /// 加载javascript脚本文件
        /// </summary>
        public static string LoadJs(string jsPath)
        {
            return @"<script type=""text/javascript"" src=""" + jsPath + @"""></script>" + "\r\n";
        }

        /// <summary>
        /// 构建Html
        /// </summary>
        /// <returns></returns>
        public static string BuildHtml()
        {
            StringBuilder sbHtml = new StringBuilder("<html>");

            sbHtml.Append("<head>");

            sbHtml.Append("<title>");
            sbHtml.Append("</title>");

            sbHtml.Append("</head>");
            sbHtml.Append("</head>");

            sbHtml.Append("<body>");
            sbHtml.Append("</body>");
            
            sbHtml.Append("</html>");

            return sbHtml.ToString();
        }


    }
}
