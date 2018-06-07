using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        [Produces("application/json", Type = typeof(Dtos.VideoRead))]        
        [Route("")]
        [HttpGet]
        public IActionResult GetByPage([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 1, int ordertype = 0)
        {
            var videos = this._respository.GetVideos(pageSize, pageSize * (pageIndex - 1), ordertype);
            var videosRead = Mapper.Map<IEnumerable<Dtos.VideoRead>>(videos);
            var code = videos.Count() > 0 ? Enums.StatusCodeEnum.OK : Enums.StatusCodeEnum.NotFound;
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = videosRead
            };
            return Json(resultMsg);
        }

        /// <summary>
        /// 根据视频 Id 获取单个视频详情
        /// </summary>
        /// <param name="id">视频 Id</param>
        /// <returns></returns>
        [Produces("application/json", Type = typeof(Dtos.VideoRead))]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var video = this._respository.GetVideo(id);
            var videoRead = Mapper.Map<Dtos.VideoRead>(video);
            var code = video != null ? Enums.StatusCodeEnum.OK : Enums.StatusCodeEnum.NotFound;
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = videoRead
            };
            return Json(resultMsg);
        }

    }
}
