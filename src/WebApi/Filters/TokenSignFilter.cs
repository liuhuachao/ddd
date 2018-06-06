using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using System.Web.Http.Filters;
using System.Threading;
using System.Web.Http.Controllers;
using WebApi.Dtos;
using WebApi.Common;
using WebApi.Enums;

namespace WebApi.Filters
{    
    public class TokenSignFilter : ActionFilterAttribute
    {
        public const int OVERTIME = 100;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ResultMsg resultMsg = null;
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

            // 判断请求头中是否包含以下所有验证参数
            if (string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(nonce) || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(signature))
            {
                resultMsg = new ResultMsg();
                resultMsg.Code = (int)StatusCodeEnum.ParameterError;
                resultMsg.Description = EnumHelper.GetEnumDescription(StatusCodeEnum.ParameterError);
                resultMsg.Data = "";
                actionContext.Response = ResponseHelper.ObjToResponse(resultMsg);
                base.OnActionExecuting(actionContext);
                return;
            }

            //判断timespan是否有效
            double ts1 = 0;
            double ts2 = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
            bool timespanvalidate = double.TryParse(timestamp, out ts1);
            double ts = ts2 - ts1;
            bool falg = ts > OVERTIME * 1000;
            if (falg || (!timespanvalidate))
            {
                resultMsg = new ResultMsg();
                resultMsg.Code = (int)StatusCodeEnum.URLExpireError;
                resultMsg.Description = EnumHelper.GetEnumDescription(StatusCodeEnum.URLExpireError);
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
