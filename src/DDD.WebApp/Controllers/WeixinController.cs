using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDD.Common;
using DDD.Common.Enums;
using DDD.Application.Dtos;

namespace DDD.WebApp.Controllers
{
    [Route("[Controller]/[Action]")]
    public class WeixinController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult JSSDKShare(string shareUrl)
        {
            var code = StatusCodeEnum.OK;
            var msg = "success";

            WeixinShare wx = new WeixinShare()
            {
                AppId = "wx53212c4f86956025",
                AppSecret = "421abb6dada3353a1f37ccf8ef5a1fe0",
                Timestamp = Common.TimeHelper.GetTimestamp(),
                NonceStr = JSSDKHelper.GetNonceStr(),
                Url = shareUrl,
            };

            var tokenDic = JSSDKHelper.GetAccessToken(wx.AppId,wx.AppSecret);
            if (tokenDic != null)
            {
                var accessToken = tokenDic["access_token"];
                if (accessToken != null && !string.IsNullOrEmpty(accessToken))
                {
                    var ticketDic = JSSDKHelper.GetTicket(accessToken);
                    if (ticketDic != null && string.IsNullOrEmpty(ticketDic["ticket"]))
                    {
                        wx.Ticket = ticketDic["ticket"];
                        wx.Signature = JSSDKHelper.CreateSign(wx.Ticket, wx.NonceStr, wx.Timestamp, wx.Url);
                    }
                    else
                    {
                        code = StatusCodeEnum.InternalServerError;
                        msg = "获取jsapi_ticket出错！";
                    }
                }
                else
                {
                    code = StatusCodeEnum.InternalServerError;
                    msg = "获取access_token出错！";
                }

                ResultMsg resultMsg = new ResultMsg()
                {
                    Code = (int)code,
                    Msg = msg,
                    Data = wx
                };

                return Json(resultMsg);

            }

            




            return View();
        }
    }
}