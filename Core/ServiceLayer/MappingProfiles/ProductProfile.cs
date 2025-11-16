using AutoMapper;
using DomainLayer.Models.ProductModels;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
	public class ProductProfile:Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductDto>()
				.ForMember(dest => dest.BrandName, option => option.MapFrom(source => source.ProductBrand.Name))
				.ForMember(dest => dest.TypeName, option => option.MapFrom(source => source.ProductType.Name))
				.ForMember(dest => dest.PictureUrl,opt => opt.MapFrom<ImageResolver>());
			CreateMap<ProductType,TypeDto>().ForMember(dest => dest.Name, option => option.MapFrom(source => source.Name));
			CreateMap<ProductBrand, BrandDto>().ForMember(dest => dest.Name, option => option.MapFrom(source => source.Name));
		}

	}
}
