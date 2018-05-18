using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Interfaces;
using WebApi.Repositories;


namespace WebApi.Controllers
{
    //[Route("v1/[controller]")]
    [Route("v1/news")]
    public class CmsContentsController : Controller
    {
        private readonly ICmsContentsRespository cms;
        private readonly PigeonsContext _context;

        public CmsContentsController(ICmsContentsRespository cmsContentRespository,PigeonsContext pigeonsContext)
        {
            cms = cmsContentRespository;
            _context = pigeonsContext;
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

        //[HttpGet("{id}")]
        [Route("{id}", Name = "GetCmsContent")]
        public IActionResult Get(int id)
        {
            //Entities.CmsContents content = this.cms.GetCmsContents(id);
            //return Ok(content);

            var content = from c in _context.CmsContents
                          where c.CmsId == id
                          select new
                          {
                              Id = c.CmsId,
                              Title = c.CmsTitle,
                              Author = c.CmsAuthor,
                              CoverImg = c.CmsPhotos,
                              PostTime = c.OprateDate,
                          };
            return Json(content);
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