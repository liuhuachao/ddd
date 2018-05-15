using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiAuth.Entities
{
    public class MyDbContext : DbContext
    {      
        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {
            //Database.EnsureCreated();
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
