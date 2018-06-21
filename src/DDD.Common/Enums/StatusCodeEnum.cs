using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DDD.Common.Enums
{
    /// <summary>
    /// 状态码
    /// </summary>
    public enum StatusCodeEnum
    {
        #region Successful 200-299
        [Description("请求成功")]
        OK = 200,

        [Description("已创建")]
        Created = 201,

        [Description("已接受")]
        Accepted = 202,

        [Description("非授权信息")]
        NonAuthoritativeInformation = 203,

        [Description("无内容")]
        NoContent = 204,

        [Description("重置内容")]
        ResetContent = 205,
        #endregion

        #region Redirection 300-399
        [Description("多种选择")]
        MultipleChoices = 300,

        [Description("永久移动")]
        Moved = 301,

        [Description("临时移动，重定向")]
        Redirect = 302,

        [Description("查看其他位置")]
        SeeOther = 303,

        [Description("未修改")]
        NotModified = 304,

        [Description("使用代理")]
        UseProxy = 305,

        [Description("临时重定向")]
        TemporaryRedirect = 307,
        #endregion

        #region Client Error 400-499 
        [Description("错误请求,参数错误!")]
        BadRequest = 400,

        [Description("未授权")]
        Unauthorized = 401,

        [Description("禁止")]
        Forbidden = 403,

        [Description("请求的资源不存在")]
        NotFound = 404,

        [Description("HTTP请求类型不合法")]
        HttpMehtodError = 405,

        [Description("HTTP请求不合法,请求参数可能被篡改")]
        HttpRequestError = 406,

        [Description("该URL已经失效")]
        URLExpireError = 407,

        [Description("请求超时")]
        RequestTimeout = 408,

        [Description("冲突")]
        Conflict = 409,
        #endregion

        #region Server Error 500-599
        [Description("服务器内部错误")]
        InternalServerError = 500,

        [Description("尚未实施")]
        NotImplemented = 501,

        [Description("错误网关")]
        BadGateway = 502,

        [Description("服务不可用")]
        ServiceUnavailable = 503,

        [Description("网关超时")]
        GatewayTimeout = 504,

        [Description("HTTP 版本不受支持")]
        HttpVersionNotSupported = 505
        #endregion
    }
}
