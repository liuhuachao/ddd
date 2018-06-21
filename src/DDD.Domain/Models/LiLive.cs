using System;
using System.Collections.Generic;

namespace DDD.Domain.Models
{
    public partial class LiLive
    {
        public int Id { get; set; }
        public string LiveNum { get; set; }
        public string Title { get; set; }
        public string CoverImg { get; set; }
        public string Organizer { get; set; }
        public int LivePlatform { get; set; }
        public string LiveJs { get; set; }
        public string Info { get; set; }
        public string LiveTime { get; set; }
        public int Sort { get; set; }
        public int Hits { get; set; }
        public int LiveStatus { get; set; }
        public int LiveDisplay { get; set; }
        public string LiveLogin { get; set; }
        public string LiveLoginPw { get; set; }
        public int LiveLoginStatus { get; set; }
        public string LiveLoginInfo { get; set; }
        public int UserId { get; set; }
        public DateTime Uptime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Status { get; set; }
        public int AuditUserId { get; set; }
        public DateTime? AuditTime { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
    }
}
