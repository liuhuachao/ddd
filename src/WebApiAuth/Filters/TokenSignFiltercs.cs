using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using System.Web.Http.Filters;
using System.Threading;
using System.Web.Http.Controllers;
using WebApiAuth.Models;
using WebApiAuth.Common;
using WebApiAuth.Enums;

namespace WebApiAuth.Filters
{
    public class TokenSignFiltercs : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Models.ResultMsg resultMsg = null;
            var request = actionContext.Request;

            string timestamp = string.Empty;
            string nonce = string.Empty;
            string token = string.Empty;
            string signature = string.Empty;

            if (request.Headers.Contains("timestamp"))
            {
                timestamp = HttpUtility.UrlDecode(request.Headers.GetValues("timestamp").FirstOrDefault());
            }
            if (request.Headers.Contains("nonce"))
            {
                nonce = HttpUtility.UrlDecode(request.Headers.GetValues("nonce").FirstOrDefault());
            }
            if (request.Headers.Contains("token"))
            {
                nonce = HttpUtility.UrlDecode(request.Headers.GetValues("token").FirstOrDefault());
            }
            if (request.Headers.Contains("signature"))
            {
                signature = HttpUtility.UrlDecode(request.Headers.GetValues("signature").FirstOrDefault());
            }

            // 判断请求头中是否包含所有验证参数
            if (string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(nonce) || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(signature))
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeEnum.ParameterError;
                resultMsg.Info = EnumHelper.GetEnumDescription(StatusCodeEnum.ParameterError);
                resultMsg.Data = "";
                actionContext.Response = ResponseHelper.ObjToResponse(resultMsg);
                base.OnActionExecuting(actionContext);
                return;
            }



            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
