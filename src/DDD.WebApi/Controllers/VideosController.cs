using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.WebApi.Filters;
using DDD.WebApi.Repositories;
using DDD.Common.Enums;
using DDD.Application.Dtos;

namespace DDD.WebApi.Controllers
{
    /// <summary>
    /// 视频
    /// </summary>
    [HiddenApi]
    [Route("v1/[Controller]/[Action]")]    
    public class VideosController : Controller
    {
        private readonly ILogger<VideosController> _logger;
        private readonly IVideosRepository _Repository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="Repository">仓库</param>
        public VideosController(ILogger<VideosController> logger, IVideosRepository Repository)
        {
            _logger = logger;
            _Repository = Repository;
        }

        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="pageSize">每页条数，默认为10</param>
        /// <param name="pageIndex">第几页，默认为1</param>
        /// <param name="ordertype">排序方式，0表示倒序,1表示正序，默认为0</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetList([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 1, int ordertype = 0)
        {
            var videos = this._Repository.GetList(pageSize, pageSize * (pageIndex - 1), ordertype);
            var videosList = Mapper.Map<IEnumerable<VideoList>>(videos);
            var code = videos.Count() > 0 ? StatusCodeEnum.OK : StatusCodeEnum.NotFound;
            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = videosList
            };
            return Json(resultMsg);
        }

        /// <summary>
        /// 根据视频 Id 获取单个视频详情
        /// </summary>
        /// <param name="id">视频 Id</param>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetDetail([FromQuery]int id)
        {
            var video = this._Repository.GetSingle(id);            
            var videoDetail = Mapper.Map<VideoDetail>(video);
            var code = video != null ? StatusCodeEnum.OK : StatusCodeEnum.NotFound;
            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = videoDetail
            };
            return Json(resultMsg);
        }

        /// <summary>
        /// 根据标题搜索视频
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns></returns>        
        [HttpGet]
        public IActionResult Search([FromQuery]string title)
        {
            var videos = this._Repository.Search(title);
            var videosList = Mapper.Map<IList<VideoList>>(videos);
            var code = videos.Count() > 0 ? StatusCodeEnum.OK : StatusCodeEnum.NotFound;
            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = videosList
            };
            return Json(resultMsg);
        }

        /// <summary>
        /// 更新点击量
        /// </summary>
        /// <param name="id">资讯 Id</param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> UpdateClicks([FromQuery]int id)
        {
            var addClick = new Random().Next(1, 10);
            var video = this._Repository.GetSingle(id);
            video.Hits += addClick;
            var code = await this._Repository.SaveAsync() > 0 ? StatusCodeEnum.OK : StatusCodeEnum.NotModified;
            var videoDetail = Mapper.Map<VideoDetail>(this._Repository.GetSingle(id));

            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = videoDetail,
            };

            return Json(resultMsg);
        }

        /// <summary>
        /// 更新点赞量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> UpdateLikes([FromQuery]int id)
        {
            var addLikes = new Random().Next(1, 10);
            var news = this._Repository.GetSingle(id);
            news.Likes += addLikes;
            var code = await this._Repository.SaveAsync() > 0 ? StatusCodeEnum.OK : StatusCodeEnum.NotModified;
            var videoDetail = Mapper.Map<VideoDetail>(this._Repository.GetSingle(id));

            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = videoDetail,
            };

            return Json(resultMsg);
        }
    }
}
