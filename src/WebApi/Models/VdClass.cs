using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class VdClass
    {
        public int Id { get; set; }
        public string NameCn { get; set; }
        public string NameEn { get; set; }
        public string ShowInfo { get; set; }
        public string PcIcon { get; set; }
        public string WapIcon { get; set; }
        public int WapStyle { get; set; }
        public int Sort { get; set; }
        public int Display { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public int MediaType { get; set; }
    }
}
