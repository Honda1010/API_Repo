using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PresistanceLayer
{
	public class DataSeeding(StoreDBContext _storeDBContext) : IDataSeeding
	{
		public void SeedData()
		{
			try
			{
				if (_storeDBContext.Database.GetPendingMigrations().Any())
				{
					_storeDBContext.Database.Migrate();
				}
				if (!_storeDBContext.ProductBrands.Any())
				{
					var brandData = File.ReadAllText(@"..\infrastructure\PresistanceLayer\Data\DataSeed\brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
					if (brands is not null && brands.Any())
					{
						_storeDBContext.ProductBrands.AddRange(brands);
					}
				}
				if (!_storeDBContext.ProductTypes.Any())
				{
					var typeData = File.ReadAllText(@"..\infrastructure\PresistanceLayer\Data\DataSeed\types.json");
					var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
					if (types is not null && types.Any())
					{
						_storeDBContext.ProductTypes.AddRange(types);
					}
				}
				if (!_storeDBContext.Products.Any())
				{
					var productData = File.ReadAllText(@"..\infrastructure\PresistanceLayer\Data\DataSeed\products.json");
					var products = JsonSerializer.Deserialize<List<Product>>(productData);
					if (products is not null && products.Any())
					{
						_storeDBContext.Products.AddRange(products);
					}
				}
				_storeDBContext.SaveChanges();
			}
			catch (Exception)
			{
				//to do
				throw;
			}

		}
	}
}
