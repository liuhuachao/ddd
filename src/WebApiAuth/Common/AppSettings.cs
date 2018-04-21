using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebApiAuth.Common
{
    public static class AppSettings
    {
        public static string UrlExpireTime { get; set; }

        private static IConfigurationSection appSections = null;

        public static string GetSetting(string key)
        {
            string str = "";
            if (appSections.GetSection(key) != null)
            {
                str = appSections.GetSection(key).Value;
            }
            return str;
        }

    }
}
