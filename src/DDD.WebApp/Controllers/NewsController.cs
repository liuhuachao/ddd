using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DDD.Data;
using DDD.Application.Interfaces;
using DDD.Application.Services;
using Microsoft.Extensions.Logging;

namespace DDD.WebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsAppService _appService;
        private readonly ILogger<NewsController> _logger;

        public NewsController(INewsAppService appService, ILogger<NewsController> logger)
        {
            this._appService = appService;
            this._logger = logger;
        }

        public IActionResult Detail(int id)
        {
            var result = this._appService.GetDetail(id);
            if (result == null)
            {
                return View("/Views/NotFound.cshtml");
            }
            return View(result);
        }

    }
}