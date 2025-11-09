using DomainLayer.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresistanceLayer.Data.Configuration
{
	public class ProductConfguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasOne(p => p.ProductBrand)
				   .WithMany()
				   .HasForeignKey(p => p.BrandId);
			builder.HasOne(p => p.ProductType)
				.WithMany()
				   .HasForeignKey(p => p.TypeId);
			builder.Property(p => p.Price)
				   .HasColumnType("decimal(10,2)");
			builder.Property(p => p.Name)
				   .HasColumnType("nvarchar(200)");
			builder.Property(p => p.Description)
				   .HasColumnType("nvarchar(500)");
		}
	}
}
