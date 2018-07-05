using DDD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDD.WebApp.Controllers
{
    public class NewsController : Controller
    {        
        private readonly ILogger<NewsController> _logger;
        private readonly INewsAppService _appService;
        private readonly IHomeAppService _homeService;

        public NewsController(ILogger<NewsController> logger, INewsAppService appService, IHomeAppService homeService)
        {
            this._logger = logger;
            this._appService = appService;
            this._homeService = homeService;            
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            this._logger.LogInformation(string.Format("获取文章详情页开始,id:{0}", id));
            var newsDetail = this._appService.GetDetail(id);
            var newsMore = this._homeService.GetMore(id,0);           
            if (newsDetail == null)
            {
                this._logger.LogInformation("获取文章详情页错误：404");
                return View("/Views/Shared/NotFound.cshtml");
            }
            else
            {
                var result = new Models.NewsDetailViewModel()
                {
                    NewsDetail = newsDetail,
                    NewsDetailMore = newsMore
                };
                this._logger.LogInformation("获取文章详情页结束");
                return View(result);
            }
            
            
        }

    }
}