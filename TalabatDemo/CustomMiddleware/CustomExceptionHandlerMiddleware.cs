using DomainLayer.Exceptions;

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
				//set Status Code to response
				httpContext.Response.StatusCode = ex switch
				{
					NotFoundException => StatusCodes.Status404NotFound,
					_ => StatusCodes.Status500InternalServerError
				};
				// set content type
				httpContext.Response.ContentType = "application/json";
				// Create response model
				var response = new
				{
					StatusCode = StatusCodes.Status500InternalServerError,
					Message = ex.Message
				};
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
		}
	}
}
