using System;
using System.Collections.Generic;

namespace WebApi.Repository.Entity
{
    public partial class VdVideoRecord
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public DateTime LastTime { get; set; }
        public int Status { get; set; }
        public int MediaType { get; set; }
    }
}
