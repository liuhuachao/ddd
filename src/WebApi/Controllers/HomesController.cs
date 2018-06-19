using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Interfaces;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>    
    [ApiVersion("1.0")]
    [Route("v1/[Controller]/[Action]")]
    public class HomesController : Controller
    {
        private readonly ILogger<HomesController> _logger;
        private readonly IHomesRepository _Repository;
        private readonly IHomeService _homeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="Repository">仓储</param>
        /// <param name="homeService">缓存</param>
        public HomesController(ILogger<HomesController> logger, IHomesRepository Repository, IHomeService homeService)
        {
            _logger = logger;
            _Repository = Repository;
            _homeService = homeService;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex">第几页，默认为1</param>
        /// <param name="pageSize">每页条数，默认为8</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", Type = typeof(HomeList))]
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
                homeList = this._homeService.GetList(pageIndex,pageSize);
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
        [HttpGet]
        [Produces("application/json", Type = typeof(HomeDetail))]
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
                detail = this._homeService.GetDetail(id,showType);
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
        [HttpGet]
        [Produces("application/json", Type = typeof(HomeSearch))]
        public IActionResult Search([FromQuery]string title)
        {
            this._logger.LogInformation("搜索开始");
            Enums.StatusCodeEnum code;
            IList<Dtos.HomeSearch> homeSearch = null;
            if (string.IsNullOrEmpty(title))
            {
                code = Enums.StatusCodeEnum.BadRequest;
            }            
            else
            {
                homeSearch = this._homeService.Search(title);
                if (homeSearch == null)
                {
                    code = Enums.StatusCodeEnum.InternalServerError;
                }
                else if (homeSearch.Count <= 0)
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
                Data = homeSearch
            };
            this._logger.LogInformation("搜索结束");
            return Json(resultMsg);
        }

        /// <summary>
        /// 获取热搜
        /// </summary>
        /// <returns></returns>
        [HttpGet]         
        [Produces("application/json", Type = typeof(HotSearch))]
        public IActionResult HotSearch()
        {
            this._logger.LogInformation("热搜开始");
            Enums.StatusCodeEnum code ;
            IList<Dtos.HotSearch> hotSearch = null;
            hotSearch = this._homeService.HotSearch();

            if (hotSearch == null)
            {
                code = Enums.StatusCodeEnum.InternalServerError;
            }
            else if (hotSearch.Count <= 0)
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
                Data = hotSearch
            };
            this._logger.LogInformation("热搜结束");
            return Json(resultMsg);
        }

        /// <summary>
        /// 更新点赞量
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="showType">显示类型，3表示视频，其他为资讯</param>
        /// <returns></returns>        
        [HttpPatch]
        [Produces("application/json", Type = typeof(ResultMsg))]
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
