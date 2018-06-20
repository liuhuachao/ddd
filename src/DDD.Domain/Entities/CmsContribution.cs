using System;
using System.Collections.Generic;

namespace DDD.Domain.Entities
{
    public partial class CmsContribution
    {
        public int Id { get; set; }
        public int Cmsid { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Autonym { get; set; }
        public string Nickname { get; set; }
        public string ContactTel { get; set; }
        public string ContactMail { get; set; }
        public int Origin { get; set; }
        public int Exclusive { get; set; }
        public int Royalty { get; set; }
        public int IsPayRoyalty { get; set; }
        public string Annotations { get; set; }
        public int SupplierId { get; set; }
        public int UserId { get; set; }
        public string MsgIp { get; set; }
        public DateTime Addtime { get; set; }
        public int Status { get; set; }
        public int? AuditUserId { get; set; }
        public DateTime? AuditTime { get; set; }
        public string SendContent { get; set; }
        public DateTime SendTime { get; set; }
    }
}
