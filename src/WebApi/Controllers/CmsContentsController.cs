using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.Models;
using WebApi.Interfaces;
using WebApi.Repositories;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    /// <summary>
    ///  资讯
    /// </summary>
    [Route("v1/news")]
    public class CmsContentsController : Controller
    {
        private readonly ICmsContentsRespository _respository;

        public CmsContentsController(ICmsContentsRespository respository)
        {
            _respository = respository;
        }

        /// <summary>
        /// 根据Id获取单篇文章
        /// </summary>
        /// <param name="id">文章Id</param>
        /// <returns>返回单篇文章</returns>
        [Route("{id}", Name = "GetCmsContent")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var content = this._respository.GetNews(id);
            return Json(content);
        }

        /// <summary>
        /// 获取图文列表页，带过滤参数
        /// </summary>
        /// <param name="limit">返回记录的数量</param>
        /// <param name="start">返回记录的开始位置</param>
        /// <param name="ordertype">返回记录的排序方法,0表示降序,1表示升序</param>
        /// <returns></returns>
        [Route("")]
        [Route("limit/{limit}")]
        [Route("limit/{limit}/start/{start}")]
        [Route("limit/{limit}/ordertype/{ordertype}")]
        [Route("limit/{limit}/start/{start}/ordertype/{ordertype}")]
        [HttpGet]
        public IActionResult Get(int limit = 10, int start = 0, int ordertype = 0)
        {
            var contents = this._respository.GetNewsList(limit, start, ordertype);
            return Json(contents);
        }

        /// <summary>
        /// 添加异步方法
        /// </summary>
        /// <param name="cmsContent"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewsCreate news)
        {
            if (news == null)
            {
                return BadRequest();
            }

            if (news.Title == "共产党")
            {
                ModelState.AddModelError("Title", "资讯的标题不可以是'共产党'三字");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var content = new CmsContents()
            {
                CmsTitle = news.Title,
                CmsKeys = news.Intro,
                CmsPhotos = news.CoverImg,
                CmsAuthor = news.Author,
                OprateDate = Convert.ToDateTime(news.PostTime),
            };

            this._respository.AddCmsContents(content);
            await this._respository.SaveAsync();
            return CreatedAtRoute("GetCmsContent", new { id =  content.CmsId }, news);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">文章Id</param>
        /// <param name="value">修改内容</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">文章Id</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
