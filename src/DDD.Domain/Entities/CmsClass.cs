using System;
using System.Collections.Generic;

namespace DDD.Domain.Entities
{
    public partial class CmsClass
    {
        public CmsClass()
        {
            CmsContents = new HashSet<CmsContents>();
        }

        public int CmsCid { get; set; }
        public string CmsCode { get; set; }
        public string CmsCname { get; set; }
        public string CmsEname { get; set; }
        public int? CmsClick { get; set; }
        public byte? CmsFlag { get; set; }
        public string CmsOrder { get; set; }
        public string CmsDesc { get; set; }
        public string CmsImg { get; set; }
        public string TargetUrl { get; set; }
        public byte? Attribs { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }

        public ICollection<CmsContents> CmsContents { get; set; }
    }
}
