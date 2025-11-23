using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModels;
using DomainLayer.Models.ProductModels;
using ServiceAbstractionLayer;
using ServiceLayer.Specifications;
using Shared.Dtos.IdentityDtos;
using Shared.Dtos.OrderDtos;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class OrderService(IMapper _mapper,IBasketRepository _basketRepository , IUnitOfWork _unitOfWork) : IOrderService
	{
		public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string UserEmail)
		{
			//create Address from orderDto.Address
			var address = _mapper.Map<AddressDto>(orderDto.Address);
			// Create Basket from orderDto.BasketId
			var basket =  await _basketRepository.GetBasketAsync(orderDto.BasketId) ?? throw new BasketNotFoundException(orderDto.BasketId);
			// Create OrderItems from basket.Items
			var ProductRepository = _unitOfWork.GetRepository<Product,int>();
			var orderItems = new List<OrderItem>();
			foreach (var item in basket.Items)
			{
				var OriginalProduct = await ProductRepository.GetByIdAsync(item.Id)?? throw new ProductNotFoundException(item.Id);
				var orderItem = new OrderItem()
				{
					Product = new ProductItemOrdered() {
						ProductId = OriginalProduct.Id,
						ProductName = OriginalProduct.Name,
						PictureUrl = OriginalProduct.PictureUrl
					},
					Price = OriginalProduct.Price,
					Quantity = item.Quantity
				};
				orderItems.Add(orderItem);
			}
			// Get DeliveryMethod from orderDto.DeliveryMethodId
			var deliveryMethodRepository = _unitOfWork.GetRepository<DeliveryMethod,int>();
			var deliveryMethod = await deliveryMethodRepository.GetByIdAsync(orderDto.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
			// Calculate Subtotal
			var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
			// Create Order
			var order = new Order(UserEmail, _mapper.Map<OrderAddress>(address), deliveryMethod, orderDto.DeliveryMethodId, orderItems, subtotal);
			// Save to Database
			await _unitOfWork.SaveChangesAsync();
			// Return OrderToReturnDto
			return _mapper.Map<OrderToReturnDto>(order);

		}

		public async Task<IEnumerable<DeliverMethodDto>> GetAllDeliveryMethodAsync()
		{
			var DeliveryMethods =  await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
			return _mapper.Map<IEnumerable<DeliverMethodDto>>(DeliveryMethods);
			
		}

		public async Task<IEnumerable<OrderToReturnDto>> GetAllOrderAsync(string UserEmail)
		{
			 var spec = new OrderSpecification(UserEmail);
			var orders = await _unitOfWork.GetRepository<Order,Guid>().GetAllAsync(spec);
			return _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);

		}

		public async Task<IEnumerable<OrderToReturnDto>> GetAllOrderById(Guid id)
		{
			var spec = new OrderSpecification(id);
			var orders =  await _unitOfWork.GetRepository<Order,Guid>().GetAllAsync(spec);
			return _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
		}
	}
}
