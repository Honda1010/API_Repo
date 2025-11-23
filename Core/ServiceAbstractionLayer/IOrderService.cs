using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
	public interface IOrderService
	{
		Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto , string UserEmail);
		Task<IEnumerable<DeliverMethodDto>> GetAllDeliveryMethodAsync();
		Task<IEnumerable<OrderToReturnDto>> GetAllOrderAsync(string UserEmail);
		Task<IEnumerable<OrderToReturnDto>> GetAllOrderById(Guid id);

	}
}
