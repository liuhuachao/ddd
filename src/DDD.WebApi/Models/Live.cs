using System;
using System.Collections.Generic;

namespace DDD.WebApi.Models
{
    public partial class Live
    {
        public int LiveId { get; set; }
        public int LiveType { get; set; }
        public string CmsCode { get; set; }
        public int? MemberId { get; set; }
        public string SupplierName { get; set; }
        public string LiveName { get; set; }
        public string LiveUrl { get; set; }
        public DateTime? LiveTime { get; set; }
        public string LiberationPoint { get; set; }
        public int? Number { get; set; }
        public int? Attribs { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public int? CreateUserid { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? UpdateUserid { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int LiveClick { get; set; }
    }
}
