using DDD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DDD.WebApi.Filters;

namespace DDD.WebApi.Controllers
{
    [HiddenApi]
    public class NewsController : Controller
    {
        private readonly INewsAppService _appService;
        private readonly ILogger<NewsController> _logger;

        public NewsController(INewsAppService appService, ILogger<NewsController> logger)
        {
            this._appService = appService;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            this._logger.LogInformation(string.Format("获取文章详情页开始,id:{0}",id));
            var result = this._appService.GetDetail(id);
            if (result == null)
            {
                return View("/Views/Shared/NotFound.cshtml");
            }
            this._logger.LogInformation("获取文章详情页结束");
            return View(result);
        }

    }
}