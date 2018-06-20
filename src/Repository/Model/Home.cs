using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repository.Model
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeList
    {
        private string _title;
        private string _postTime;

        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }     
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImg { get; set; }
        /// <summary>
        /// 视频源Url
        /// </summary>
        public string SourceUrl { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 发表时间
        /// </summary>
        public string PostTime { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 显示类型，可选值为：0/1/2/3，0表示上图+下文，1表示左图+右文，2表示无图纯文，3表示视频
        /// </summary>
        public int ShowType { get; set; }
    }

    /// <summary>
    /// 详情
    /// </summary>
    public class HomeDetail
    {
        private string _title;
        private string _intro;
        private string _content;

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
        /// <summary>
        /// 视频源Url
        /// </summary>
        public string SourceUrl { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 显示类型，可选值为：0/1/2/3，0表示上图+下文，1表示左图+右文，2表示无图纯文，3表示视频
        /// </summary>
        public int? ShowType { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int? Clicks { get; set; }
        /// <summary>
        /// 点赞量
        /// </summary>
        public int? Likes { get; set; }
        /// <summary>
        /// 分享链接
        /// </summary>
        public string ShareLink { get; set; }
    }

    /// <summary>
    /// 搜索
    /// </summary>
    public class HomeSearch
    {
        private string _title;

        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 显示类型，可选值为：0/1/2/3，0表示上图+下文，1表示左图+右文，2表示无图纯文，3表示视频
        /// </summary>
        public int? ShowType { get; set; }
    }

    /// <summary>
    /// 热搜
    /// </summary>
    public class HotSearch
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 显示类型，可选值为：0/1/2/3，0表示上图+下文，1表示左图+右文，2表示无图纯文，3表示视频
        /// </summary>
        public int? ShowType { get; set; }
    }
}
