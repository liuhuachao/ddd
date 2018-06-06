using System;
using System.Collections.Generic;
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
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private readonly ICmsContentsRespository _respository;
        
        /// <summary>
        /// 资讯构造函数
        /// </summary>
        /// <param name="respository"></param>
        public NewsController(ILogger<NewsController> logger,ICmsContentsRespository respository)
        {
            _logger = logger;
            _respository = respository;
        }

        /// <summary>
        /// 获取资讯列表
        /// </summary>
        /// <param name="limit">返回记录的数量，默认为10</param>
        /// <param name="start">返回记录的开始位置，默认为0</param>
        /// <param name="ordertype">返回记录的排序方法,0表示降序,1表示升序，默认为0</param>
        /// <returns></returns>        
        [Produces("application/json", Type = typeof(NewsList))]
        [Route("")]
        //[Route("limit/{limit}")]
        [HttpGet]
        public IActionResult Get(int limit = 10, int start = 0, int ordertype = 0)
        {
            this._logger.LogInformation("获取资讯列表");
            var contents = this._respository.GetCmsContents(limit, start, ordertype);
            var newsList = Mapper.Map<IEnumerable<Dtos.NewsList>>(contents);
            return Json(newsList);
        }
    
        /// <summary>
        /// 获取资讯列表，带分页功能
        /// </summary>
        /// <param name="pageSize">每页条数，默认为10</param>
        /// <param name="pageIndex">第几页，默认为1</param>
        /// <param name="ordertype">排序方式，0表示倒序,1表示正序，默认为0</param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagesize/{pageSize}/pageindex/{pageIndex}")]
        public IActionResult GetByPage(int pageSize = 10, int pageIndex = 1, int ordertype = 0)
        {
            var contents = this._respository.GetCmsContents(pageSize, pageSize*(pageIndex-1), ordertype);
            var newsList = Mapper.Map<IEnumerable<Dtos.NewsList>>(contents);
            return Json(newsList);
        }

        /// <summary>
        /// 根据Id获取单篇文章
        /// </summary>
        /// <param name="id">文章Id</param>
        /// <returns>返回单篇文章</returns>
        [Produces("application/json", Type = typeof(NewsDetail))]
        [Route("{id}", Name = "GetNews")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var content = this._respository.GetCmsContent(id);
            var results = Mapper.Map<Dtos.NewsDetail>(content);
            return Json(results);
        }



    }
}
