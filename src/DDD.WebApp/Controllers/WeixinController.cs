using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDD.Common;

namespace DDD.WebApp.Controllers
{
    public class WeixinController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JSSDKShare(string shareUrl)
        {
            WeixinShare wx = new WeixinShare()
            {
                AppId = "",
                Timestamp = Common.TimeHelper.GetTimestamp(),
                NonceStr = JSSDKHelper.GetNonceStr(8),
                
            };


            return View();
        }
    }
}