using Azure.Core;
using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.ComponentModel;

namespace TalabatDemo.CustomMiddleware
{
	public class CustomExceptionHandlerMiddleware
	{
		public RequestDelegate _next { get; set; }
		public ILogger<CustomExceptionHandlerMiddleware> _Logger { get; set; }
		public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger) {
			_next = next;
			_Logger = logger;
		}
		public async Task InvokeAsync(HttpContext httpContext) {
			try
			{
				await _next(httpContext);
				await NotFoundErrorHandler(httpContext);
			}
			catch (Exception ex)
			{
				_Logger.LogError(ex, ex.Message);
				await ErrorHandler(httpContext, ex);
			}

			static async Task ErrorHandler(HttpContext httpContext, Exception ex)
			{
				// set content type
				httpContext.Response.ContentType = "application/json";
				// Create response model
				var response = new ErrorToReturn
				{
					Message = ex.Message
				};
				//set Status Code to response
				httpContext.Response.StatusCode = ex switch
				{
					NotFoundException => StatusCodes.Status404NotFound,
					UnAuthorizedException => StatusCodes.Status401Unauthorized,
					BadRequestException badRequestException => BadRequestHandler(badRequestException, response),
					_ => StatusCodes.Status500InternalServerError
				};
				response.StatusCode= httpContext.Response.StatusCode;
				// write response to http context
				await httpContext.Response.WriteAsJsonAsync(response);
			}

			static async Task NotFoundErrorHandler(HttpContext httpContext)
			{
				if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
				{

					var response = new
					{
						StatusCode = StatusCodes.Status404NotFound,
						Message = $"This endpoint {httpContext.Request.Path} is not found"
					};
					await httpContext.Response.WriteAsJsonAsync(response);
				}
			}
			static int BadRequestHandler(BadRequestException badRequest, ErrorToReturn response) {
				response.Errors= badRequest.Errors;
				return StatusCodes.Status400BadRequest;
			}
		}
	}
}
