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
    /// 首页
    /// </summary>
    [Route("v1/[Controller]/[Action]")]
    [ApiVersion("1.0")]
    public class HomesController : Controller
    {
        private readonly ILogger<HomesController> _logger;
        private readonly IHomesRespository _respository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="respository">仓库</param>
        public HomesController(ILogger<HomesController> logger, IHomesRespository respository)
        {
            _logger = logger;
            _respository = respository;
        }


        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">标识Id</param>
        /// <param name="showType">显示类型</param>
        /// <returns></returns>
        [Produces("application/json", Type = typeof(HomeDetail))]
        [HttpGet]
        public IActionResult GetDetail([FromQuery]int id, [FromQuery]int showType)
        {
            var detail = this._respository.GetDetail(id,showType);
            var code = detail != null ? Enums.StatusCodeEnum.OK : Enums.StatusCodeEnum.NotFound;
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = detail
            };
            return Json(resultMsg);
        }

        /// <summary>
        /// 获取热搜
        /// </summary>
        /// <returns></returns>
        [Produces("application/json", Type = typeof(HomeHotSearch))]
        [HttpGet]
        public IActionResult GetHotSearch()
        {
            this._logger.LogInformation("热搜开始");
            Enums.StatusCodeEnum code ;
            var homeList = this._respository.GetList();
            if (homeList == null)
            {
                code = Enums.StatusCodeEnum.InternalServerError;
            }
            else if (homeList.Count <= 0)
            {
                code = Enums.StatusCodeEnum.NotFound;
            }
            else
            {
                code = Enums.StatusCodeEnum.OK;
            }
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = Common.EnumHelper.GetEnumDescription(code),
                Data = homeList
            };
            this._logger.LogInformation("热搜结束");
            return Json(resultMsg);
        }


    }
}
