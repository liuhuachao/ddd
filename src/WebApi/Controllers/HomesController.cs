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
    /// 热搜
    /// </summary>
    [Route("v1/homes")]
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
        /// 热搜
        /// </summary>
        /// <returns></returns>
        [Produces("application/json", Type = typeof(HomeHotSearch))]
        [Route("HotSearch")]
        [HttpGet]
        public IActionResult HotSearch()
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
            this._logger.LogInformation(string.Format("热搜返回数据：{0}",resultMsg));
            return Json(resultMsg);
        }


    }
}
