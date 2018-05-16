using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCore.WebApi.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }
    }

    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasOne(x => x.Product).WithMany(x => x.Materials).HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
