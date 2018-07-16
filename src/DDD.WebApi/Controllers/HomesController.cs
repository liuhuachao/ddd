using DDD.Application.Dtos;
using DDD.Application.Interfaces;
using DDD.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDD.WebApi.Filters;

namespace DDD.WebApi.Controllers
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
        private readonly IHomeAppService _homeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="Repository">仓储</param>
        /// <param name="homeService">缓存</param>
        public HomesController(ILogger<HomesController> logger, IHomesRepository Repository, IHomeAppService homeService)
        {
            _logger = logger;
            _Repository = Repository;
            _homeService = homeService;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex">第几页，默认为1</param>
        /// <param name="pageSize">每页条数，默认为5</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", Type = typeof(HomeList))]
        public IActionResult GetList([FromQuery]int pageIndex = 1, [FromQuery]int pageSize = 10)
        {
            this._logger.LogInformation("获取列表开始");
            pageSize = 10;
            var code = StatusCodeEnum.OK;
            IList<HomeList> homeList = null;
            if (!ModelState.IsValid || pageIndex <= 0 || pageSize <= 0)
            {
                code = StatusCodeEnum.BadRequest;
            }
            else
            {
                homeList = this._homeService.GetList(pageIndex,pageSize);
                code = (homeList == null || homeList.Count <= 0) ? StatusCodeEnum.NotFound : code;
            }
            var msg = Common.EnumHelper.GetEnumDescription(code);
            ResultMsg resultMsg = new ResultMsg()
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
            showType = (showType == 3) ? 3 : 0;
            var code = StatusCodeEnum.OK;
            HomeDetail detail = null;
            if (!ModelState.IsValid || id <= 0)
            {
                code = StatusCodeEnum.BadRequest;
            }
            else
            {
                detail = this._homeService.GetDetail(id,showType);
                code = (detail == null ) ? StatusCodeEnum.NotFound : code;
            }
            var msg = Common.EnumHelper.GetEnumDescription(code);

            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = msg,
                Data = detail
            };
            this._logger.LogInformation("获取详情结束");
            return Json(resultMsg);
        }

        /// <summary>
        /// 更多精彩
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="showType">显示类型，3表示视频，其他为资讯</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", Type = typeof(HomeList))]
        public IActionResult GetMore([FromQuery]int id, [FromQuery]int showType)
        {
            this._logger.LogInformation("获取更多精彩开始");
            showType = (showType == 3) ? 3 : 0;
            var code = StatusCodeEnum.OK;
            IList<HomeList> homeMore = null;
            if (!ModelState.IsValid || id <= 0)
            {
                code = StatusCodeEnum.BadRequest;
            }
            else
            {
                homeMore = this._homeService.GetMore(id, showType);
                code = (homeMore == null) ? StatusCodeEnum.NotFound : code;
            }
            var msg = Common.EnumHelper.GetEnumDescription(code);

            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = msg,
                Data = homeMore
            };
            this._logger.LogInformation("获取更多精彩结束");
            return Json(resultMsg);
        }

        /// <summary>
        /// 搜索标题
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns></returns>        
        [HttpGet]
        [Route("/v1/homes/search")]
        [Route("/v1/videos/search")]
        [Produces("application/json", Type = typeof(HomeList))]
        public IActionResult Search([FromQuery]string title)
        {
            this._logger.LogInformation("搜索开始");
            StatusCodeEnum code;
            IList<HomeList> homeSearch = null;
            if (string.IsNullOrEmpty(title))
            {
                code = StatusCodeEnum.BadRequest;
            }            
            else
            {
                homeSearch = this._homeService.Search(title);
                if (homeSearch == null)
                {
                    code = StatusCodeEnum.InternalServerError;
                }
                else if (homeSearch.Count <= 0)
                {
                    code = StatusCodeEnum.NotFound;
                }
                else
                {
                    code = StatusCodeEnum.OK;
                }
            }
            var msg = Common.EnumHelper.GetEnumDescription(code);
            ResultMsg resultMsg = new ResultMsg()
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
        [Produces("application/json", Type = typeof(HomeList))]
        public IActionResult HotSearch()
        {
            this._logger.LogInformation("热搜开始");
            StatusCodeEnum code ;
            IList<HomeList> hotSearch = null;
            hotSearch = this._homeService.HotSearch();

            if (hotSearch == null)
            {
                code = StatusCodeEnum.InternalServerError;
            }
            else if (hotSearch.Count <= 0)
            {
                code = StatusCodeEnum.NotFound;
            }
            else
            {
                code = StatusCodeEnum.OK;
            }
            var msg = Common.EnumHelper.GetEnumDescription(code);
            ResultMsg resultMsg = new ResultMsg()
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
            StatusCodeEnum code = StatusCodeEnum.OK;
            if (!ModelState.IsValid || id <= 0 )
            {
                code = StatusCodeEnum.BadRequest;
            }
            else if (!this._Repository.IsExist(id, showType))
            {
                code = StatusCodeEnum.NotFound;
            }
            else
            {
                code = await this._Repository.UpdateLikes(id,showType) > 0 ? code : StatusCodeEnum.NotModified;
            }
            var msg = Common.EnumHelper.GetEnumDescription(code);

            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = msg,
                Data = null,
            };

            this._logger.LogInformation("更新点赞量结束");
            return Json(resultMsg);
        }

        /// <summary>
        /// 删除首页列表和详情的缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="showType"></param>
        /// <returns></returns>
        [HttpDelete]
        [HiddenApi]
        public IActionResult RemoveCache([FromQuery]int id, [FromQuery]int showType)
        {
            this._logger.LogInformation("删除首页列表和详情缓存开始");
            var code = StatusCodeEnum.OK;
            if (!ModelState.IsValid || id <= 0)
            {
                code = StatusCodeEnum.BadRequest;
            }
            else
            {
                this._homeService.RemoveCache(id, showType);
            }
            var msg = Common.EnumHelper.GetEnumDescription(code);
            ResultMsg resultMsg = new ResultMsg()
            {
                Code = (int)code,
                Msg = msg,
                Data = null
            };
            this._logger.LogInformation("删除首页列表和详情缓存结束");
            return Json(resultMsg);
        }
    }
}
