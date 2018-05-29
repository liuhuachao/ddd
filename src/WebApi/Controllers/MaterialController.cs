using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    // 和主Model的Controller前缀一样
    [Route("v1/product")] 
    public class MaterialController : Controller
    {
        /// <summary>
        /// 根据产品Id获取物料列表
        /// </summary>
        /// <param name="productId">产品Id</param>
        /// <returns>返回产品列表</returns>
        [HttpGet("{productId}/material")]
        public IActionResult GetMaterials(int productId)
        {
            var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.Materials);
        }

        /// <summary>
        /// 根据物料Id获取单个材料
        /// </summary>
        /// <param name="productId">产品Id</param>
        /// <param name="id">物料Id</param>
        /// <returns>返回单个物料</returns>
        [HttpGet("{productId}/material/{id}")]
        public IActionResult GetMaterial(int productId, int id)
        {
            var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == productId);
            if (product == null)
            {
                return NotFound();
            }
            var material = product.Materials.SingleOrDefault(x => x.Id == id);
            if (material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }
    }
}