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
		public async Task SeedDataAsync()
		{
			try
			{
				if ((await _storeDBContext.Database.GetPendingMigrationsAsync()).Any())
				{
					await _storeDBContext.Database.MigrateAsync();
				}
				if (!_storeDBContext.ProductBrands.Any())
				{
					var brandData = File.OpenRead(@"..\infrastructure\PresistanceLayer\Data\DataSeed\brands.json");
					var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(brandData);
					if (brands is not null && brands.Any())
					{
						_storeDBContext.ProductBrands.AddRange(brands);
					}
				}
				if (!_storeDBContext.ProductTypes.Any())
				{
					var typeData = File.OpenRead(@"..\infrastructure\PresistanceLayer\Data\DataSeed\types.json");
					var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(typeData);
					if (types is not null && types.Any())
					{
						_storeDBContext.ProductTypes.AddRange(types);
					}
				}
				if (!_storeDBContext.Products.Any())
				{
					var productData = File.OpenRead(@"..\infrastructure\PresistanceLayer\Data\DataSeed\products.json");
					var products = await JsonSerializer.DeserializeAsync<List<Product>>(productData);
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
