using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiAuth.Models;
using WebApiAuth.Services;

namespace WebApiAuth.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        // GET api/products
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ProductService.Current.Products);
        }

        // GET api/products/id
        [Route("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/products
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (product.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maxId = ProductService.Current.Products.Max(x => x.Id);
            var newProduct = new Product
            {
                Id = ++maxId,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
            ProductService.Current.Products.Add(newProduct);

            return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);
        }
    }
}
