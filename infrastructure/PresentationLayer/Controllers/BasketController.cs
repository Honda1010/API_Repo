using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.Dtos.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BasketController(IServiceManager _serviceManager): ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<ActionResult<BasketDto>> GetBasketByIdAsync(string id)
		{
			var basket = await _serviceManager.BasketService.GetBasketAsync(id);
			return Ok(basket);
		}
		[HttpPost]
		public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basketDto)
		{
			var basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basketDto);
			return Ok(basket);
		}
		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
		{
			var res = await _serviceManager.BasketService.DeleteBasketAsync(id);
			return Ok(res);
		}
	}
}
