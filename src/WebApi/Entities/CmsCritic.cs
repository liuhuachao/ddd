using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public partial class CmsCritic
    {
        public int CommId { get; set; }
        public int? CmsId { get; set; }
        public string CriticTitle { get; set; }
        public string CriticBody { get; set; }
        public int? UserId { get; set; }
        public DateTime? OprateDate { get; set; }

        public CmsContents Cms { get; set; }
    }
}
