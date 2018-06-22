using AutoMapper;
using DDD.Application.Dtos;
using DDD.Common.Enums;
using DDD.Domain.Interfaces;
using DDD.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.WebApi.Controllers
{
    /// <summary>
    ///  资讯
    /// </summary>    
    [Route("v1/news")]
    [ApiVersion("1.0")]
    [HiddenApi]
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private readonly ICmsContentsRepository _Repository;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="Repository">仓库</param>
        public NewsController(ILogger<NewsController> logger,ICmsContentsRepository Repository)
        {
            _logger = logger;
            _Repository = Repository;
        }

        /// <summary>
        /// 获取资讯列表
        /// </summary>
        /// <param name="pageSize">每页条数，默认为10</param>
        /// <param name="pageIndex">第几页，默认为1</param>
        /// <param name="ordertype">返回记录的排序方法,0表示降序,1表示升序，默认为0</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [HiddenApi]
        public IActionResult GetList([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 1, int ordertype = 0)
        {
            var contents = this._Repository.GetList(pageSize, pageSize * (pageIndex - 1), 0);
            var newsList = Mapper.Map<IEnumerable<NewsList>>(contents);
            var code = contents.Count() > 0 ? StatusCodeEnum.OK : StatusCodeEnum.NotFound;
            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = newsList
            };
            return Json(resultMsg);
        }

        /// <summary>
        /// 根据 Id 获取单篇文章详情
        /// </summary>
        /// <param name="id">文章Id</param>
        /// <returns>返回单篇文章</returns>        
        [HttpGet]
        [Route("{id}", Name = "GetDetail")]
        public IActionResult GetDetail(int id)
        {
            var content = this._Repository.GetDetail(id);
            var newsDetail = Mapper.Map<NewsDetail>(content);
            var code = content != null ? StatusCodeEnum.OK : StatusCodeEnum.NotFound;
            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = newsDetail
            };
            return Json(resultMsg);
        }

        /// <summary>
        /// 根据标题搜索资讯
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns></returns>        
        [HttpGet]
        [Route("Search")]
        public IActionResult Search([FromQuery]string title)
        {
            var contents = this._Repository.Search(title);
            var newsList = Mapper.Map<IList<NewsList>>(contents);
            var code = contents.Count() > 0 ? StatusCodeEnum.OK : StatusCodeEnum.NotFound;
            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = newsList
            };
            return Json(resultMsg);
        }

        /// <summary>
        /// 更新点击量
        /// </summary>
        /// <param name="id">资讯 Id</param>
        /// <returns></returns>        
        [HttpPatch]
        [Route("UpdateClicks")]
        public async Task<IActionResult> UpdateClicks([FromQuery]int id)
        {
            var addClick = new Random().Next(1,10);
            var news = this._Repository.GetDetail(id);
            news.CmsClick += addClick;
            var code = await this._Repository.SaveAsync() > 0 ? StatusCodeEnum.OK : StatusCodeEnum.NotModified;
            var newsDetail = Mapper.Map<NewsDetail>(this._Repository.GetDetail(id));

            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = newsDetail,
            };

            return Json(resultMsg);
        }

        /// <summary>
        /// 更新点赞量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("UpdateLikes")]
        public async Task<IActionResult> UpdateLikes([FromQuery]int id)
        {
            var addLikes = new Random().Next(1, 10);
            var news = this._Repository.GetDetail(id);
            news.Likes += addLikes;
            var code = await this._Repository.SaveAsync() > 0 ? StatusCodeEnum.OK : StatusCodeEnum.NotModified;
            var newsDetail = Mapper.Map<NewsDetail>(this._Repository.GetDetail(id));

            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = newsDetail,
            };

            return Json(resultMsg);
        }
    }
}
