using System;
using System.Collections.Generic;

namespace DDD.Domain.Entities
{
    public partial class CmsAuthor
    {
        public int Id { get; set; }
        public string Coverimg { get; set; }
        public string Autonym { get; set; }
        public string Nickname { get; set; }
        public string ContactTel { get; set; }
        public string ContactMail { get; set; }
        public string Info { get; set; }
        public int Articles { get; set; }
        public int Hits { get; set; }
        public int TruthHits { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Rec { get; set; }
        public int Sort { get; set; }
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int AuditUserId { get; set; }
        public DateTime AuditTime { get; set; }
        public int Status { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
    }
}
