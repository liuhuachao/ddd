using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebApi.Enums
{
    public enum StatusCodeEnum
    {
        [Description("请求成功")]
        Success = 200, 

        [Description("未授权标识")]
        Unauthorized = 401,

        [Description("请求参数不完整或不正确")]
        ParameterError = 400,

        [Description("请求TOKEN失效")]
        TokenInvalid = 403,

        [Description("找不到请求的页面")]
        NotFound = 404,

        [Description("HTTP请求类型不合法")]
        HttpMehtodError = 405,

        [Description("HTTP请求不合法,请求参数可能被篡改")]
        HttpRequestError = 406,

        [Description("该URL已经失效")]
        URLExpireError = 407,

        [Description("内部请求出错")]
        Error = 500,
    }
}
