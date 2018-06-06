﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public VideosController(ILogger<VideosController> logger, IVideosRespository respository)
        {
            _logger = logger;
            _respository = respository;
        }

        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="limit">返回记录的数量，默认为10</param>
        /// <param name="start">返回记录的开始位置，默认为0</param>
        /// <param name="ordertype">返回记录的排序方法,0表示降序,1表示升序，默认为0</param>
        /// <returns></returns>  
        [Produces("application/json", Type = typeof(Dtos.VideosRead))]
        [Route("")]
        [HttpGet]
        public IActionResult Get(int limit = 10, int start = 0, int ordertype = 0)
        {
            this._logger.LogInformation("获取视频列表");
            var contents = this._respository.GetVideos(limit, start, ordertype);
            var newsList = Mapper.Map<IEnumerable<Dtos.VideosRead>>(contents);
            return Ok(newsList);
        }

        /// <summary>
        /// 获取视频列表，带分页功能
        /// </summary>
        /// <param name="pageSize">每页条数，默认为10</param>
        /// <param name="pageIndex">第几页，默认为1</param>
        /// <param name="ordertype">排序方式，0表示倒序,1表示正序，默认为0</param>
        /// <returns></returns>
        [Produces("application/json", Type = typeof(Dtos.VideosRead))]
        [HttpGet]
        [Route("pagesize/{pageSize}/pageindex/{pageIndex}")]
        public IActionResult GetByPage(int pageSize = 10, int pageIndex = 1, int ordertype = 0)
        {
            var contents = this._respository.GetVideos(pageSize, pageSize * (pageIndex - 1), ordertype);
            var videoList = Mapper.Map<IEnumerable<Dtos.VideosRead>>(contents);
            return Ok(videoList);
        }

        /// <summary>
        /// 根据视频 Id 获取单个视频详情
        /// </summary>
        /// <param name="id">视频 Id</param>
        /// <returns></returns>
        [Produces("application/json", Type = typeof(Dtos.VideosRead))]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var video = this._respository.GetVideo(id);
            var VideosRead = Mapper.Map<Dtos.VideosRead>(video);
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)Enums.StatusCodeEnum.Success,
                Description = Common.EnumHelper.GetEnumDescription(Enums.StatusCodeEnum.Success),
                Data = VideosRead
            };
            return Ok(resultMsg);
        }

    }
}
