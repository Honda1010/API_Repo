using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
			Services.AddScoped<IDataSeeding, DataSeeding>();
			Services.AddScoped<IUnitOfWork, UnitOfWork>();
			Services.AddScoped<IBasketRepository,BasketRepository>();
			Services.AddSingleton<IConnectionMultiplexer>((_) => {
				var connectionString = Configuration.GetConnectionString("RedisConnection");
				return ConnectionMultiplexer.Connect(connectionString);
			});
			return Services;
		}
	}
}
