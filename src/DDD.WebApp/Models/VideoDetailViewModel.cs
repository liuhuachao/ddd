using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Application.Dtos;

namespace DDD.WebApp.Models
{
    public class VideoDetailViewModel
    {
        public VideoDetail VideoDetail { get; set; }
        public IList<HomeList> VideoDetailMore { get; set; }
    }
}
