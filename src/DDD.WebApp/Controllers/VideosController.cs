using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDD.Data;
using DDD.Application.Dtos;

namespace DDD.WebApp.Controllers
{
    public class VideosController : Controller
    {
        private readonly PigeonsContext _pigeonContext;

        public VideosController(PigeonsContext pigeonContext)
        {
            this._pigeonContext = pigeonContext;
        }

        public IActionResult Detail(int id)
        {
            var video = this._pigeonContext.VdVideo.Find(id);
            var videoDetail = AutoMapper.Mapper.Map<VideoDetail>(video);
            return View(videoDetail);
        }
    }
}