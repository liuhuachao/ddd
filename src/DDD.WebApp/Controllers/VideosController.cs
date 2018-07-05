using DDD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDD.WebApp.Controllers
{
    public class VideosController : Controller
    {
        private readonly ILogger<VideosController> _logger;
        private readonly IVideosAppService _appService;
        private readonly IHomeAppService _homeService;
        

        public VideosController(ILogger<VideosController> logger, IVideosAppService appService,IHomeAppService homeService)
        {            
            this._logger = logger;
            this._appService = appService;
            this._homeService = homeService;
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            this._logger.LogInformation(string.Format("获取视频详情页开始,id:{0}", id));
            var detail = this._appService.GetDetail(id);
            var more = this._homeService.GetMore(id, 3);            
            if (detail == null)
            {
                this._logger.LogInformation("获取视频详情页出错：404");
                return View("/Views/Shared/NotFound.cshtml");
            }
            else
            {
                var result = new Models.VideoDetailViewModel()
                {
                    VideoDetail = detail,
                    VideoDetailMore = more
                };
                this._logger.LogInformation("获取视频详情页结束");
                return View(result);
            }
            
            
        }
    }
}