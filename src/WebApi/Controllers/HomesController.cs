using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Interfaces;
using WebApi.Repositories;

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
        private readonly IHomesRepository _Repository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="Repository">仓库</param>
        public HomesController(ILogger<HomesController> logger, IHomesRepository Repository)
        {
            _logger = logger;
            _Repository = Repository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex">第几页，默认为1</param>
        /// <param name="pageSize">每页条数，默认为8</param>
        /// <returns></returns>
        [Produces("application/json", Type = typeof(HomeList))]
        [HttpGet]
        public IActionResult GetList([FromQuery]int pageIndex = 1, [FromQuery]int pageSize = 8)
        {
            this._logger.LogInformation("获取列表开始");
            var code = Enums.StatusCodeEnum.OK;
            IList<Dtos.HomeList> homeList = null;
            if (!ModelState.IsValid || pageIndex <= 0 || pageSize <= 0)
            {
                code = Enums.StatusCodeEnum.BadRequest;
            }
            else
            {
                homeList = this._Repository.GetList(pageIndex,pageSize);
                code = (homeList == null || homeList.Count <= 0) ? Enums.StatusCodeEnum.NotFound : code;
            }
            var msg = Common.EnumHelper.GetEnumDescription(code);
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = msg,
                Data = homeList
            };
            this._logger.LogInformation("获取列表结束");
            return Json(resultMsg);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="showType">显示类型，3表示视频，其他为资讯</param>
        /// <returns></returns>
        [Produces("application/json", Type = typeof(HomeDetail))]
        [HttpGet]
        public IActionResult GetDetail([FromQuery]int id, [FromQuery]int showType)
        {
            this._logger.LogInformation("获取详情开始");
            var code = Enums.StatusCodeEnum.OK;
            Dtos.HomeDetail detail = null;
            if (!ModelState.IsValid || id <= 0)
            {
                code = Enums.StatusCodeEnum.BadRequest;
            }
            else
            {
                detail = this._Repository.GetDetail(id, showType);
                code = (detail == null ) ? Enums.StatusCodeEnum.NotFound : code;
            }
            var msg = Common.EnumHelper.GetEnumDescription(code);

            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = msg,
                Data = detail
            };
            this._logger.LogInformation("获取详情结束");
            return Json(resultMsg);
        }

        /// <summary>
        /// 搜索标题
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns></returns>
        [Produces("application/json", Type = typeof(HomeSearch))]
        [HttpGet]
        public IActionResult Search([FromQuery]string title)
        {
            this._logger.LogInformation("搜索开始");
            Enums.StatusCodeEnum code;
            IList<Dtos.HomeSearch> homeList = null;
            if (string.IsNullOrEmpty(title))
            {
                code = Enums.StatusCodeEnum.BadRequest;
            }            
            else
            {
                homeList = this._Repository.Search(title);
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
            }
            var msg = Common.EnumHelper.GetEnumDescription(code);
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = msg,
                Data = homeList
            };
            this._logger.LogInformation("搜索结束");
            return Json(resultMsg);
        }

        /// <summary>
        /// 获取热搜
        /// </summary>
        /// <returns></returns>
        [Produces("application/json", Type = typeof(HotSearch))]
        [HttpGet]
        public IActionResult HotSearch()
        {
            this._logger.LogInformation("热搜开始");
            Enums.StatusCodeEnum code ;
            var homeList = this._Repository.HotSearch();
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
            var msg = Common.EnumHelper.GetEnumDescription(code);
            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = msg,
                Data = homeList
            };
            this._logger.LogInformation("热搜结束");
            return Json(resultMsg);
        }
        
        /// <summary>
        /// 更新点赞量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="showType"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> UpdateLikes([FromQuery]int id, [FromQuery]int showType)
        {
            this._logger.LogInformation("更新点赞量开始");
            Enums.StatusCodeEnum code = Enums.StatusCodeEnum.OK;

            if (!ModelState.IsValid || id <= 0 )
            {
                code = Enums.StatusCodeEnum.BadRequest;
            }
            else if (!this._Repository.IsExist(id, showType))
            {
                code = Enums.StatusCodeEnum.NotFound;
            }
            else
            {
                code = await this._Repository.UpdateLikes(id,showType) > 0 ? code : Enums.StatusCodeEnum.NotModified;
            }
            var msg = Common.EnumHelper.GetEnumDescription(code);

            Dtos.ResultMsg resultMsg = new Dtos.ResultMsg()
            {
                Code = (int)code,
                Msg = msg,
                Data = null,
            };

            this._logger.LogInformation("更新点赞量结束");
            return Json(resultMsg);
        }
    }
}
