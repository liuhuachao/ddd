using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Dtos
{
    /// <summary>
    /// 资讯查询列表类
    /// </summary>    
    public class NewsList
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }      
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImg { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 发表时间
        /// </summary>
        public string PostTime { get; set; }
    }

    /// <summary>
    /// 资讯查询详情类
    /// </summary>    
    public class NewsDetail
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImg { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 发表时间
        /// </summary>
        public string PostTime { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 资讯新增类
    /// </summary>
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
