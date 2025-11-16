using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PresistanceLayer.Identity;
using PresistanceLayer.Reposirtory;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresistanceLayer
{
	public static class InfrastructureServiceRegisteration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services , IConfiguration Configuration)
		{
			Services.AddDbContext<StoreDBContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			});
			Services.AddDbContext<StoreIdentityDBContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
			});
			Services.AddScoped<IDataSeeding, DataSeeding>();
			Services.AddScoped<IUnitOfWork, UnitOfWork>();
			Services.AddScoped<IBasketRepository,BasketRepository>();
			// Redis Configuration
			Services.AddSingleton<IConnectionMultiplexer>((_) => {
				var connectionString = Configuration.GetConnectionString("RedisConnection");
				return ConnectionMultiplexer.Connect(connectionString);
			});
			// Identity Configuration
			Services.AddIdentityCore<ApplicationUser>()
				.AddRoles<IdentityRole>().
				AddEntityFrameworkStores<StoreIdentityDBContext>();

			return Services;
		}
	}
}
