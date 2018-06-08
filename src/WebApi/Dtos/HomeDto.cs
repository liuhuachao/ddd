using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeList
    {
        private string _postTime;
        private string _className;

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
        public string PostTime
        {
            get
            {
                if (string.IsNullOrEmpty(_postTime) || !Common.TimeHelper.IsDate(_postTime))
                    return "刚刚";
                else
                    return Common.TimeHelper.GetTimeDiffUntil(Convert.ToDateTime(_postTime));
            }
            set
            {
                _postTime = value;
            }
        }
        /// <summary>
        /// 栏目编码
        /// </summary>
        public string ClassCode { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ClassName
        {
            get { return Common.Utility.GetClassName(ClassCode); }
            set { _className = value; }
        }
        /// <summary>
        /// 显示类型，可选值为：0/1/2/3，0表示上图+下文，1表示左图+右文，2表示无图纯文，3表示视频
        /// </summary>
        public int ShowType
        {
            get
            {
                return string.IsNullOrEmpty(this.CoverImg) ? 2 : new Random().Next(0, 2);
            }
            set { }
        }
    }
}
