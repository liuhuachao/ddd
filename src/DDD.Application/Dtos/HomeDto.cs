using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Common;

namespace DDD.Application.Dtos
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeList
    {
        private string _title;
        private string _intro;
        private string _postTime;

        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return HtmlHelper.TransformHtml(_title); }
            set { _title = value; }
        }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro
        {
            get { return HtmlHelper.Decode(_intro); }
            set { _intro = value; }
        }
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
        public string PostTime
        {
            get
            {
                if (!string.IsNullOrEmpty(_postTime) && Common.TimeHelper.IsDate(_postTime))
                {
                    return Common.TimeHelper.GetTimeDiffUntil(Convert.ToDateTime(_postTime));
                }
                else if (string.IsNullOrEmpty(_postTime))
                {
                    return "刚刚";
                }
                else return _postTime;                    
            }
            set
            {
                _postTime = value;
            }
        }
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
        public string Title
        {
            get { return HtmlHelper.TransformHtml(_title); }
            set { _title = value; }
        }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro
        {
            get { return HtmlHelper.TransformHtml(_intro); }
            set { _intro = value; }
        }
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
        public string Content
        {
            get
            {
                return HtmlHelper.BuildNewsDetailHtml(_content);
            }
            set
            {
                _content = value;
            }
        }
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
        public string Title
        {
            get { return HtmlHelper.TransformHtml(_title); }
            set { _title = value; }
        }
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
