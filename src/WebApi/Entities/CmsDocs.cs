using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public partial class CmsDocs
    {
        public int CommId { get; set; }
        public int? CmsId { get; set; }
        public string DocsType { get; set; }
        public string DocsPath { get; set; }
        public string SourceName { get; set; }
        public int? UserId { get; set; }
        public DateTime? OprateDate { get; set; }
    }
}
