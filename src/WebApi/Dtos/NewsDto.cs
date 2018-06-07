using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Dtos
{   
    /// <summary>
    /// 资讯列表
    /// </summary>    
    public class NewsList
    {
        private string _coverImage;
        private int _showType;

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
        public string CoverImg
        {
            get { return _coverImage; }
            set { _coverImage = value; }
        }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 发表时间
        /// </summary>
        public string PostTime { get; set; }
        /// <summary>
        /// 显示类型，可选值为：0/1/2/3，0表示上图+下文，1表示左图+右文，2表示无图纯文，3表示视频
        /// </summary>
        public int ShowType
        {
            get
            {                
                return string.IsNullOrEmpty(this._coverImage)?2:new Random().Next(0,2);
            }
            set { _showType = value; }
        }

    }

    /// <summary>
    /// 资讯详情
    /// </summary>    
    public class NewsDetail
    {
        private int _id;
        private string _shareLink = "http://m.chsgw.com/news/news_detail.aspx?id=";
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
        /// 点击量
        /// </summary>
        public int Clicks { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public int Likes { get; set; }
        /// <summary>
        /// 分享链接
        /// </summary>
        public string ShareLink
        {
            get
            {
                return _shareLink + this.Id.ToString();
            }
            set { _shareLink = value; }
        }



    }
}
