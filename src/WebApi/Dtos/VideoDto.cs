using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    /// <summary>
    /// 视频列表
    /// </summary>    
    public class VideoList
    {
        private int _showType = 3;

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
        /// 视频源Url
        /// </summary>
        public string SourceUrl { get; set; }
        /// <summary>
        /// 显示类型，可选值为：0/1/2/3，0表示上图+下文，1表示左图+右文，2表示无图纯文，3表示视频
        /// </summary>
        public int ShowType
        {
            get { return _showType; }
            set { _showType = value; }
        }
    }

    /// <summary>
    /// 视频详情
    /// </summary>    
    public class VideoDetail
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
        /// 视频源Url
        /// </summary>
        public string SourceUrl { get; set; }
    }

}
