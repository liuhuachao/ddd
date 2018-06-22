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
        private readonly INewsAppService _newsAppService;
        private readonly ILogger<NewsController> _logger;

        public NewsController(INewsAppService newsAppService, ILogger<NewsController> logger)
        {
            this._newsAppService = newsAppService;
            this._logger = logger;
        }

        public IActionResult Detail(int id)
        {
            var news = this._newsAppService.GetDetail(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

    }
}