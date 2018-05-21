using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Interfaces;
using WebApi.Repositories;
using AutoMapper;

namespace WebApi.Controllers
{
    [Route("v1/news")]
    public class CmsContentsController : Controller
    {
        private readonly ICmsContentsRespository _respository;
        private readonly PigeonsContext _context;

        public CmsContentsController(ICmsContentsRespository respository, PigeonsContext context)
        {
            _respository = respository;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var content = (from c in _context.CmsContents                          
                          orderby c.CmsId descending
                          select new
                          {
                              Id = c.CmsId,
                              Title = c.CmsTitle,
                              Author = c.CmsAuthor,
                              CoverImg = c.CmsPhotos,
                              PostTime = c.OprateDate,
                          })
                          .Take(10);
            return Json(content);
        }

        [Route("{id}", Name = "GetCmsContent")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var content = from c in _context.CmsContents
                          where c.CmsId == id
                          select new
                          {
                              cmsid = c.CmsId,
                              title = c.CmsTitle,
                              author = c.CmsAuthor,
                              coverimg = c.CmsPhotos,
                              posttime = c.OprateDate,
                          };
            return Json(content);
        }

        /// <summary>
        /// 获取图文列表，带分页功能
        /// </summary>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">第几页</param>
        /// <returns></returns>
        [Route("pagesize/{pageSize}/pageindex/{pageIndex}")]
        [HttpGet]
        public IActionResult GetByPager(int pageSize = 10, int pageIndex = 1)
        {
            var contents = this._respository.GetNewsList(pageSize, pageSize * (pageIndex - 1));
            return Json(contents);
        }

        /// <summary>
        /// 获取图文列表页，带过滤参数
        /// </summary>
        /// <param name="limit">返回记录的数量</param>
        /// <param name="start">返回记录的开始位置</param>
        /// <param name="ordertype">返回记录的排序方法,0表示降序,1表示升序</param>
        /// <returns></returns>
        [Route("limit/{limit}")]
        [Route("limit/{limit}/start/{start}")]
        [Route("limit/{limit}/start/{start}/ordertype/{ordertype}")]
        [HttpGet]
        public IActionResult GetByParams(int limit = 10, int start = 1, int ordertype = 0)
        {
            var contents = this._respository.GetNewsList(limit, start, ordertype);
            return Json(contents);
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// 添加异步方法
        /// </summary>
        /// <param name="cmsContent"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CmsContents cmsContent)
        {
            if (cmsContent == null)
            {
                return BadRequest();
            }

            if (cmsContent.CmsTitle == "共产党")
            {
                ModelState.AddModelError("Title", "资讯的标题不可以是'共产党'三字");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this._context.CmsContents.Add(cmsContent);
            await this._context.SaveChangesAsync();

            return CreatedAtRoute("GetCmsContent", new { id = cmsContent.CmsId }, cmsContent);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}