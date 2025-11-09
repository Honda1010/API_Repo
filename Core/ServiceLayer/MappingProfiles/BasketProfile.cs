using AutoMapper;
using DomainLayer.Models.BasketModels;
using Shared.Dtos.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
	public class BasketProfile: Profile
	{
		public BasketProfile()
		{
			CreateMap<Basket, BasketDto>().ReverseMap();
			CreateMap<BasketItem, BasketItemDto>().ReverseMap();
		}
	}
}
