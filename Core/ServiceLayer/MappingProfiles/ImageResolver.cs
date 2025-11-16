using AutoMapper;
using DomainLayer.Models.ProductModels;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
	internal class ImageResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDto, String>
	{
		public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrWhiteSpace(source.PictureUrl))
			{
				return $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
			}
			return string.Empty;
		}
	}
}
