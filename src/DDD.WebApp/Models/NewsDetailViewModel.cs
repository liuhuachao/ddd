using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Common;

namespace DDD.WebApp.Models
{
    /// <summary>
    /// 详情
    /// </summary>
    public class NewsDetailViewModel
    {
        private string _title;
        private string _intro;
        private string _author = "尊贵赛鸽";
        private string _content;

        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return HtmlHelper.Decode(_title); }
            set { _title = value; }
        }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro
        {
            get { return Common.HtmlHelper.Decode(_intro); }
            set { _intro = value; }
        }
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImg { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
        /// <summary>
        /// 发表时间
        /// </summary>
        public string PostTime { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content
        {
            get
            {
                return HtmlHelper.Decode(_content);
            }
            set
            {
                _content = value;
            }
        }
        /// <summary>
        /// 点击量
        /// </summary>
        public int? Clicks { get; set; }
        /// <summary>
        /// 点赞量
        /// </summary>
        public int? Likes { get; set; }
    }
}
