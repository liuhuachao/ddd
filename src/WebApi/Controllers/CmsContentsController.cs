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
        private readonly ICmsContentsRespository _cms;

        public CmsContentsController(ICmsContentsRespository cms)
        {
            _cms = cms;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("1");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Entities.CmsContents content = this._cms.GetCmsContents(id);
            return Ok(content);
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
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