using DomainLayer.Contracts;

namespace TalabatDemo.Extensions
{
	public static class WebApplicationRegisteration
	{
		public async static Task SeedDataAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			await scope.ServiceProvider.GetRequiredService<IDataSeeding>().SeedDataAsync();
			await scope.ServiceProvider.GetRequiredService<IDataSeeding>().SeedIdentityDataAsync();
		}
	}
}
