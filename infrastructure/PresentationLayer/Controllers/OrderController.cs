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
	[Authorize]
	public class OrderController(IServiceManager serviceManager) :ApiControllerBase
	{
		[HttpPost]
	
		public async Task<ActionResult<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto)
		{
			var OrderToReturnDto = await serviceManager.OrderService.CreateOrderAsync(orderDto,GetUserEmail());
			return Ok(OrderToReturnDto);
		}

		[HttpGet("deliveryMethods")]
		[AllowAnonymous]
		public async Task<ActionResult<IEnumerable<DeliverMethodDto>>> GetAllDeliveryMethodAsync()
		{
			var deliveryMethods = await serviceManager.OrderService.GetAllDeliveryMethodAsync();
			return Ok(deliveryMethods);
		}
		[HttpGet]
	
		public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrderAsync()
		{
			var orders = await serviceManager.OrderService.GetAllOrderAsync(GetUserEmail());
			return Ok(orders);
		}
		[HttpGet("{id}")]
	
		public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrderById(Guid id)
		{
			var orders = await serviceManager.OrderService.GetAllOrderById(id);
			return Ok(orders);
		}
	}
}
