using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDD.Common;
using DDD.Common.Enums;
using DDD.Application.Dtos;
using Microsoft.Extensions.Logging;
using DDD.WebApi.Filters;

namespace DDD.WebApi.Controllers
{
    [HiddenApi]
    [Route("v1/[Controller]/[Action]")]
    public class WeixinController : Controller
    {
        private readonly ILogger<HomesController> _logger;

        public WeixinController(ILogger<HomesController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult JSSDKShare(string shareUrl)
        {
            this._logger.LogInformation("获取微信分享js-sdk 开始");

            var code = StatusCodeEnum.OK;
            var msg = "success";
            ResultMsg resultMsg = new ResultMsg();

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
                this._logger.LogInformation(string.Format("获取微信分享js-sdk的accessToken为：{0}", accessToken.ToString()));
                if (accessToken != null && !string.IsNullOrEmpty(accessToken))
                {                    
                    var ticketDic = JSSDKHelper.GetTicket(accessToken);
                    this._logger.LogInformation(string.Format("获取微信分享js-sdk的jsapi_ticket为：{0}",JsonHelper.DicToJson(ticketDic)));
                    if (ticketDic != null && !string.IsNullOrEmpty(ticketDic["ticket"]))
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

                resultMsg = new ResultMsg()
                {
                    Code = (int)code,
                    Msg = msg,
                    Data = wx
                };                
            }

            this._logger.LogInformation("获取微信分享js-sdk 结束");
            return Json(resultMsg);
        }
    }
}