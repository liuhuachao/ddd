using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Application.Dtos;

namespace DDD.WebApp.Models
{
    public class NewsDetailViewModel
    {
        public NewsDetail NewsDetail { get; set; }
        public IList<HomeList> NewsDetailMore { get; set; }
    }
}
