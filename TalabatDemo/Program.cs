
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens.Experimental;
using PresistanceLayer;
using PresistanceLayer.Reposirtory;
using ServiceAbstractionLayer;
using ServiceLayer; 
using System.Threading.Tasks;
using TalabatDemo.CustomMiddleware;
using Shared.ErrorModels;
using TalabatDemo.Factory;
using TalabatDemo.Extensions;

namespace TalabatDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
			// Register Swagger Services
            builder.Services.AddSwaggerServices();

			// Register Infrastrucure Services
			builder.Services.AddInfrastructureServices(builder.Configuration);
			// Register Application Services
			builder.Services.AddApplicationServices();


			// register Web Application Services
			builder.Services.AddWebApplicationServices();




			var app = builder.Build();

            await app.SeedDataAsync();

			// Configure the HTTP request pipeline.
			// Use Custom Exception Handling Middleware
			app.UseMiddleware<CustomExceptionHandlerMiddleware>();
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
