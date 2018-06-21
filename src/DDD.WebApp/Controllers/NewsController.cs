using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDD.Data;
using AutoMapper;

namespace DDD.WebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly PigeonsContext _pigeonContext;

        public NewsController(PigeonsContext pigeonContext)
        {
            this._pigeonContext = pigeonContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            var contents = this._pigeonContext.CmsContents.Find(id);
            var news = Mapper.Map<Models.NewsDetailViewModel>(contents);
            return View(news);
        }
    }
}