using System;
using System.Collections.Generic;

namespace DDD.WebApi.Models
{
    public partial class VdBanner
    {
        public int Id { get; set; }
        public int Cid { get; set; }
        public int Sid { get; set; }
        public string Title { get; set; }
        public string CoverImg { get; set; }
        public string WebLinks { get; set; }
        public string WapLinks { get; set; }
        public int Sort { get; set; }
        public DateTime Uptime { get; set; }
        public int Status { get; set; }
        public int MediaType { get; set; }
    }
}
