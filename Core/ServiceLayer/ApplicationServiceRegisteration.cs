using Microsoft.Extensions.DependencyInjection;
using ServiceAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public static class ApplicationServiceRegisteration
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
		{
			Services.AddScoped<IProductService, ProductService>();
			Services.AddScoped<IServiceManager, ServiceManager>();
			Services.AddAutoMapper((x) => { }, typeof(ServiceLayerAssemblyReference).Assembly);
			return Services;
		}
	}
}
