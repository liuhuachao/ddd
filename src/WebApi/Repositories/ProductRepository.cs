using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Interfaces;

namespace WebApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsContext _MyDbContext;

        public ProductRepository(ProductsContext MyDbContext)
        {
            _MyDbContext = MyDbContext;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _MyDbContext.Products.OrderBy(x => x.Name).ToList();
        }

        public Product GetProduct(int productId, bool includeMaterials)
        {
            if (includeMaterials)
            {
                return _MyDbContext.Products.Include(x => x.Materials).FirstOrDefault(x => x.Id == productId);
            }
            return _MyDbContext.Products.Find(productId);
        }

        public IEnumerable<Material> GetMaterialsForProduct(int productId)
        {
            return _MyDbContext.Materials.Where(x => x.ProductId == productId).ToList();
        }

        public Material GetMaterialForProduct(int productId, int materialId)
        {
            return _MyDbContext.Materials.FirstOrDefault(x => x.ProductId == productId && x.Id == materialId);
        }

        public bool ProductExist(int productId)
        {
            return _MyDbContext.Products.Any(x => x.Id == productId);
        }

        public void AddProduct(Product product)
        {
            _MyDbContext.Products.Add(product);
        }

        public bool Save()
        {
            return _MyDbContext.SaveChanges() >= 0;
        }

        public void DeleteProduct(Product product)
        {
            _MyDbContext.Products.Remove(product);
        }

    }
}
