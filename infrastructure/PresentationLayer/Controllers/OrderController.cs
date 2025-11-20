using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
	public class OrderController(IServiceManager serviceManager) :ApiControllerBase
	{
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto)
		{
			var OrderToReturnDto = await serviceManager.OrderService.CreateOrderAsync(orderDto,GetUserEmail());
			return Ok(OrderToReturnDto);
		}

	}
}
