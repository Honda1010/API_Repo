
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using PresistanceLayer;
using PresistanceLayer.Reposirtory;
using ServiceAbstractionLayer;
using ServiceLayer; 
using System.Threading.Tasks;

namespace TalabatDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

			builder.Services.AddAutoMapper((x) => { },typeof(ServiceLayerAssemblyReference).Assembly);



			var app = builder.Build();

            using var scope = app.Services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<IDataSeeding>().SeedDataAsync();


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            
            app.UseStaticFiles();
			app.MapControllers();

            app.Run();
        }
    }
}
