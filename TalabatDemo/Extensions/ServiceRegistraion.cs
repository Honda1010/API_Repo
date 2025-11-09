using Microsoft.AspNetCore.Mvc;
using TalabatDemo.Factory;

namespace TalabatDemo.Extensions
{
	public static class ServiceRegistraion
	{
		public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			Services.AddEndpointsApiExplorer();
			Services.AddSwaggerGen();
			return Services;
		}
		public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services)
		{
			// Customize validation error response using ApiResponseFactory
			Services.Configure<ApiBehaviorOptions>((options) => {
				options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
			});
			return Services;
		}
	}
}
