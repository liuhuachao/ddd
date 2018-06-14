using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    /// <summary>
    /// 视频
    /// </summary>
    [Route("v1/videos")]
    public class VideosController : Controller
    {
        private readonly ILogger<VideosController> _logger;
        private readonly IVideosRespository _respository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="respository">仓库</param>
        public VideosController(ILogger<VideosController> logger, IVideosRespository respository)
        {
            _logger = logger;
            _respository = respository;
        }

        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="pageSize">每页条数，默认为10</param>
        /// <param name="pageIndex">第几页，默认为1</param>
        /// <param name="ordertype">排序方式，0表示倒序,1表示正序，默认为0</param>
        /// <returns></returns>
        [Produces("application/json", Type = typeof(Dtos.VideoList))]        
        [Route("")]
        [HttpGet]
        public IActionResult GetList([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 1, int ordertype = 0)
        {
            var videos = this._respository.GetList(pageSize, pageSize * (pageIndex - 1), ordertype);
            var videosList = Mapper.Map<IEnumerable<Dtos.VideoList>>(videos);
            var code = videos.Count() > 0 ? Enums.StatusCodeEnum.OK : Enums.StatusCodeEnum.NotFound;
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
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
        [Produces("application/json", Type = typeof(Dtos.VideoDetail))]
        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
        {
            var video = this._respository.GetSingle(id);            
            var videoDetail = Mapper.Map<Dtos.VideoDetail>(video);
            var code = video != null ? Enums.StatusCodeEnum.OK : Enums.StatusCodeEnum.NotFound;
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
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
        [Produces("application/json", Type = typeof(Dtos.VideoList))]
        [Route("Search")]
        [HttpGet]
        public IActionResult Search([FromQuery]string title)
        {
            var videos = this._respository.Search(title);
            var videosList = Mapper.Map<IList<Dtos.VideoList>>(videos);
            var code = videos.Count() > 0 ? Enums.StatusCodeEnum.OK : Enums.StatusCodeEnum.NotFound;
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
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
        [Route("UpdateClicks")]
        [HttpPatch]
        public async Task<IActionResult> UpdateClicks([FromQuery]int id)
        {
            var addClick = new Random().Next(1, 10);
            var video = this._respository.GetSingle(id);
            video.Hits += addClick;
            var code = await this._respository.SaveAsync() > 0 ? Enums.StatusCodeEnum.OK : Enums.StatusCodeEnum.NotModified;
            var videoDetail = Mapper.Map<Dtos.VideoDetail>(this._respository.GetSingle(id));

            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
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
        [Route("UpdateLikes")]
        [HttpPatch]
        public async Task<IActionResult> UpdateLikes([FromQuery]int id)
        {
            var addLikes = new Random().Next(1, 10);
            var news = this._respository.GetSingle(id);
            news.Likes += addLikes;
            var code = await this._respository.SaveAsync() > 0 ? Enums.StatusCodeEnum.OK : Enums.StatusCodeEnum.NotModified;
            var videoDetail = Mapper.Map<Dtos.VideoDetail>(this._respository.GetSingle(id));

            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = videoDetail,
            };

            return Json(resultMsg);
        }

    }
}
