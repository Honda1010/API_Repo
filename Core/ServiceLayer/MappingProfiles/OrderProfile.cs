using AutoMapper;
using DomainLayer.Models.OrderModels;
using Shared.Dtos.IdentityDtos;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
	public class OrderProfile : Profile
	{
		public OrderProfile() { 
			CreateMap<Order, OrderToReturnDto>()
				.ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName));
			CreateMap<OrderAddress, AddressDto>().ReverseMap();
			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
				.ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<orderImageResolver>());
			CreateMap<DeliveryMethod, DeliverMethodDto>().ReverseMap();
		}
	}
}
