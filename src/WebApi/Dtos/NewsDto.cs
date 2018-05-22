using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class NewsRead
    {
        public int NewsId { get; set; }        
        public string Title { get; set; }
        public string Intro { get; set; }
        public string CoverImg { get; set; }
        public string Author { get; set; }
        public string PostTime { get; set; }
    }

    public class NewsCreate
    {
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50,MinimumLength = 2)]
        public string Title { get; set; }
        public string Intro { get; set; }
        public string CoverImg { get; set; }
        [Required(ErrorMessage = "{0}是必填项")]
        public string Author { get; set; }
        public string PostTime { get; set; }
        [Display(Name = "分类")]
        [Required(ErrorMessage = "{0}是必填项")]
        public string Category { get; set; }
    }
}
