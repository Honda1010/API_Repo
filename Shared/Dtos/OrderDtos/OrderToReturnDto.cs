using Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OrderDtos
{
	public class OrderToReturnDto
	{
		public Guid Id { get; set; }
		public string UserEmail { get; set; } = null!;
		public DateTimeOffset OrderDate { get; set; }
		public decimal Subtotal { get; set; }
		public decimal Total { get; set; }
		public string OrderStatus { get; set; } = null!;
		public string DeliveryMethod { get; set; } = null!; //Name of the delivery method

		public AddressDto Address { get; set; } = null!;
		public ICollection<OrderItemDto> Items { get; set; } = [];



	}
}
