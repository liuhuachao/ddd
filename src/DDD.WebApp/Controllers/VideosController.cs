using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Data;
using Microsoft.AspNetCore.Mvc;

namespace DDD.WebApp.Controllers
{
    public class VideosController : Controller
    {
        private readonly PigeonsContext _pigeonContext;

        public VideosController(PigeonsContext pigeonContext)
        {
            this._pigeonContext = pigeonContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            var video = this._pigeonContext.VdVideo.Find(id);
            var videoDetail = AutoMapper.Mapper.Map<Models.VideoDetailViewModel>(video);
            return View(videoDetail);
        }
    }
}