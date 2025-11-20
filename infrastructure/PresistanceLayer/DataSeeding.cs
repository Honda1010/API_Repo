using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using DomainLayer.Models.OrderModels;
using DomainLayer.Models.ProductModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PresistanceLayer
{
	public class DataSeeding(StoreDBContext _storeDBContext,RoleManager<IdentityRole> _roleManager ,UserManager<ApplicationUser> _userManager) : IDataSeeding
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
				if (!_storeDBContext.Set<DeliveryMethod>().Any())
				{
					var DeliverytData = File.OpenRead(@"..\infrastructure\PresistanceLayer\Data\DataSeed\delivery.json");
					var deliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliverytData);
					if (deliveryMethods is not null && deliveryMethods.Any())
					{
						_storeDBContext.Set<DeliveryMethod>().AddRange(deliveryMethods);
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

		public async Task SeedIdentityDataAsync()
		{
			try
			{
				if (!_roleManager.Roles.Any())
				{
					var roles = new List<IdentityRole>
				{
					new IdentityRole{ Name="Admin"},
					new IdentityRole{ Name="SuperAdmin"},
				};
					foreach (var role in roles)
					{
						await _roleManager.CreateAsync(role);
					}
				}
				if (!_userManager.Users.Any())
				{
					var user01 = new ApplicationUser
					{
						Email = "mohanedmohamed267@gmail.com",
						DisplayName = "Mohaned Mohamed",
						UserName = "Mohaned267",
						PhoneNumber = "01140014117"
					};
					var user02 = new ApplicationUser
					{
						Email = "maimohamed99@gmail.com",
						DisplayName = "Mai Mohamed",
						UserName = "Mai267",
						PhoneNumber = "01234567890"
					};
					foreach (var user in new List<ApplicationUser> { user01, user02 })
					{
						var result = await _userManager.CreateAsync(user, "P@ssw0rd");
					}
					await _userManager.AddToRoleAsync(user01, "Admin");
					await _userManager.AddToRoleAsync(user02, "SuperAdmin");

				}
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
