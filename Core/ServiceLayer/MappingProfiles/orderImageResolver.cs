using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
	public class orderImageResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
	{
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrWhiteSpace(source.Product.PictureUrl))
			{
				return $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
			}
			return string.Empty;
		}
	}
}
