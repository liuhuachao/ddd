using DDD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DDD.WebApi.Filters;

namespace DDD.WebApi.Controllers
{
    [HiddenApi]
    public class VideosController : Controller
    {
        private readonly IVideosAppService _appService;
        private readonly ILogger<VideosController> _logger;

        public VideosController(IVideosAppService appService, ILogger<VideosController> logger)
        {
            this._appService = appService;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var result = this._appService.GetDetail(id);
            if (result == null)
            {
                return View("/Views/Shared/NotFound.cshtml");
            }
            return View(result);
        }
    }
}