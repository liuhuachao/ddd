using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public partial class VdVideoReview
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public int ToUserId { get; set; }
        public int ReId { get; set; }
        public int ReId2 { get; set; }
        public int ReId3 { get; set; }
        public string ReContent { get; set; }
        public string MsgIp { get; set; }
        public DateTime Uptime { get; set; }
        public int Status { get; set; }
        public int Agree { get; set; }
        public int Disagree { get; set; }
        public int? AuditUserId { get; set; }
        public DateTime? AuditTime { get; set; }
        public int MediaType { get; set; }
        public int NewReply { get; set; }
        public int IsRead { get; set; }
    }
}
