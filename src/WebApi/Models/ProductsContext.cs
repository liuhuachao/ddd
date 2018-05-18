using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class ProductsContext : DbContext
    {      
        public ProductsContext(DbContextOptions<ProductsContext> options): base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Material> Materials { get; set; }

    }
}
