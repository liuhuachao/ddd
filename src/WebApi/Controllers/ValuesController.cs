using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Deprecated参数的用法：
    /// 当支持多个 API 版本时, 某些版本最终会随着时间的推移而被弃用。要标记一个或多个 api 版本已被废弃, 只需用Deprecated修饰您的控制器。这并不意味着不支持 API 版本。你仍然可以调用该版本。它只是一种让 调用API 用户意识到以下版本在将来会被弃用。
    /// </summary>
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet, MapToApiVersion("1.0")]
        public string Get()
        {
            //通过下面的方法可以获取到客户端在访问那个版本的接口，当用户都升级的时候可以终止老版本的接口
            return HttpContext.GetRequestedApiVersion().ToString();
        }

        [HttpGet, MapToApiVersion("2.0")]
        public string Getv2()
        {
            return HttpContext.GetRequestedApiVersion().ToString();
        }

        [HttpGet, MapToApiVersion("3.0")]
        public string Getv3()
        {
            return HttpContext.GetRequestedApiVersion().ToString();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
