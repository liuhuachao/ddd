using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Controllers
{
    /// <summary>
    ///  资讯
    /// </summary>    
    [Route("v1/news")]
    [ApiVersion("1.0")]
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private readonly ICmsContentsRespository _respository;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="respository">仓库</param>
        public NewsController(ILogger<NewsController> logger,ICmsContentsRespository respository)
        {
            _logger = logger;
            _respository = respository;
        }        

        /// <summary>
        /// 获取资讯列表
        /// </summary>
        /// <param name="pageSize">每页条数，默认为10</param>
        /// <param name="pageIndex">第几页，默认为1</param>
        /// <param name="ordertype">返回记录的排序方法,0表示降序,1表示升序，默认为0</param>
        /// <returns></returns>
        [Route("")]        
        [HttpGet]
        public IActionResult GetNewsList([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 1, int ordertype = 0)
        {
            var contents = this._respository.GetCmsContents(pageSize, pageSize * (pageIndex - 1), 0);
            var newsList = Mapper.Map<IEnumerable<Dtos.NewsList>>(contents);
            var code = contents.Count() > 0 ? Enums.StatusCodeEnum.OK : Enums.StatusCodeEnum.NotFound;
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = newsList
            };
            return Json(resultMsg);
        }

        /// <summary>
        /// 根据Id获取单篇文章
        /// </summary>
        /// <param name="id">文章Id</param>
        /// <returns>返回单篇文章</returns>
        [Produces("application/json", Type = typeof(NewsDetail))]
        [Route("{id}", Name = "GetNews")]
        [HttpGet]
        public IActionResult GetNews(int id)
        {
            var content = this._respository.GetCmsContent(id);
            var newsDetail = Mapper.Map<Dtos.NewsDetail>(content);
            var code = content != null ? Enums.StatusCodeEnum.OK : Enums.StatusCodeEnum.NotFound;
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = newsDetail
            };
            return Json(resultMsg);
        }

    }
}
