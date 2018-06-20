using System;
using System.Collections.Generic;

namespace DDD.Domain.Entities
{
    public partial class TpTopic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string CoverImg { get; set; }
        public string BannerImg { get; set; }
        public string LinkUrl { get; set; }
        public int Hits { get; set; }
        public int TruthHits { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int SupplierId { get; set; }
        public int AddUserId { get; set; }
        public DateTime AddTime { get; set; }
        public int UpdateId { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Display { get; set; }
        public int Status { get; set; }
        public int? AuditUserId { get; set; }
        public DateTime AuditTime { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public int? TemplateId { get; set; }
        public string News1Id { get; set; }
        public string News2Id { get; set; }
        public string News3Id { get; set; }
        public string News4Id { get; set; }
        public string News5Id { get; set; }
        public int? Album1Id { get; set; }
        public int? Album2Id { get; set; }
        public int? Album3Id { get; set; }
        public int? VideoDisplay { get; set; }
        public int? LiveDisplay { get; set; }
        public int? LiveId { get; set; }
        public string TimeInfo { get; set; }
        public int Sort { get; set; }
        public string TemplateColor { get; set; }
    }
}
