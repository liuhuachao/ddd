using System;
using System.Collections.Generic;

namespace DDD.WebApi.Models
{
    public partial class CmsContents
    {
        public CmsContents()
        {
            CmsCritic = new HashSet<CmsCritic>();
        }

        public int CmsId { get; set; }
        public string CmsCode { get; set; }
        public string CmsTitleForDisp { get; set; }
        public string CmsTitle { get; set; }
        public string CmsAuthor { get; set; }
        public string CmsBody { get; set; }
        public string CmsKeys { get; set; }
        public string CmsPhotos { get; set; }
        public int? CmsStats { get; set; }
        public int? CmsCid { get; set; }
        public int? UserId { get; set; }
        public int? MemberId { get; set; }
        public int? SupplierId { get; set; }
        public string AreaCode { get; set; }
        public byte? Attribs { get; set; }
        public DateTime? OprateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CmsSource { get; set; }
        public string VideoPath { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public int CmsClick { get; set; }
        public int MasterRecommend { get; set; }
        public int FromMasterId { get; set; }
        public int Likes { get; set; }
        public int TopicId { get; set; }
        public string Wordpress { get; set; }
        public byte OriginalStatementType { get; set; }
        public decimal CmsSort { get; set; }

        public CmsClass CmsC { get; set; }
        public ICollection<CmsCritic> CmsCritic { get; set; }
    }
}
