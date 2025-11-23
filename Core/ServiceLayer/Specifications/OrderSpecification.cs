using DomainLayer.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Specifications
{
	public class OrderSpecification : BaseSpecification<Order,Guid>
	{
		public OrderSpecification(string userEmail)
			: base(o => o.UserEmail == userEmail)
		{
			AddInclude(o => o.DeliveryMethod);
			AddInclude(o => o.Items);
			AddOrderByDescending(o => o.OrderDate);
		}
		public OrderSpecification(Guid id)
			: base(o => o.Id == id)
		{
			AddInclude(o => o.DeliveryMethod);
			AddInclude(o => o.Items);
			AddOrderByDescending(o => o.OrderDate);
		}
	}
}
