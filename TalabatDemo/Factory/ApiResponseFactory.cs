using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace TalabatDemo.Factory
{
	public static class ApiResponseFactory
	{
		public static IActionResult GenerateApiValidationErrorResponse(ActionContext context) {
			var errors = context.ModelState.Where(e => e.Value.Errors.Any()).Select(m => new Shared.ErrorModels.ValidationError()
			{
				Field = m.Key,
				Errors = m.Value.Errors.Select(e => e.ErrorMessage)
			});
			var response = new ValidationErrorToReturn()
			{
				Errors = errors
			};
			return new BadRequestObjectResult(response);
		}
	}
}
