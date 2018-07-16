using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Common
{
    public class UrlHelper
    {
        public static string Encode(string url)
        {
            var returnUrl = System.Web.HttpUtility.UrlEncode(url);
            return returnUrl;
        }

        public static string Decode(string url)
        {
            var returnUrl = System.Web.HttpUtility.UrlDecode(url);
            return returnUrl;
        }
    }
}
