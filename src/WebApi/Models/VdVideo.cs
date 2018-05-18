using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class VdVideo
    {
        public int Id { get; set; }
        public int Cid { get; set; }
        public int Sid { get; set; }
        public string Tags { get; set; }
        public string CoverImg { get; set; }
        public string VideoSource { get; set; }
        public string VideoReference { get; set; }
        public string VideoReferenceVid { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string VideoLength { get; set; }
        public int VideoAutomate { get; set; }
        public int Sort { get; set; }
        public int HotTopic { get; set; }
        public int Rec { get; set; }
        public int Hits { get; set; }
        public int TruthHits { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public int SupplierId { get; set; }
        public int UserId { get; set; }
        public DateTime Uptime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Status { get; set; }
        public int? AuditUserId { get; set; }
        public DateTime? AuditTime { get; set; }
        public string AuditReply { get; set; }
        public string Moderator { get; set; }
        public string Theme { get; set; }
        public string AirTime { get; set; }
        public string GuestPerformers { get; set; }
        public int TopicId { get; set; }
        public int MediaType { get; set; }
    }
}
