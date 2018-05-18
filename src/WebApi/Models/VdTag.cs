using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class VdTag
    {
        public int Id { get; set; }
        public string NameCn { get; set; }
        public string NameEn { get; set; }
        public int Sort { get; set; }
        public int Display { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public int MediaType { get; set; }
    }
}
