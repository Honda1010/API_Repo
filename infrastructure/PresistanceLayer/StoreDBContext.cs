using DomainLayer.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresistanceLayer
{
	public class StoreDBContext : DbContext
	{
		public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
		
		public DbSet<ProductBrand> ProductBrands { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		override protected void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDBContext).Assembly);
		}

	}
}
