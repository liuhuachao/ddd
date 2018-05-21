using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class NewsRead
    {
        public int NewsId { get; set; }        
        public string Title { get; set; }
        public string Intro { get; set; }
        public string CoverImg { get; set; }
        public string Author { get; set; }
        public string PostTime { get; set; }
    }
}
