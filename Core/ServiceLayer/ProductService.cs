using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModels;
using ServiceAbstractionLayer;
using ServiceLayer.Specifications;
using Shared.Classes;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
	{
		public async Task<IEnumerable<BrandDto>> GetAllProductBrandsAsync()
		{
			var repo = _unitOfWork.GetRepository<ProductBrand, int>();
			var brands = await repo.GetAllAsync();
			// Mapping logic from ProductBrand to BrandDto would go here
			var brandDtos = _mapper.Map<IEnumerable<BrandDto>>(brands);
			return brandDtos;
		}

		public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
		{
			var specification = new ProductSpecification(queryParams);
			var repo = _unitOfWork.GetRepository<Product, int>();
			var products = await repo.GetAllAsync(specification);
			// Mapping logic from Product to ProductDto would go here
			var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
			var countSpecification = new ProductSpecification(queryParams);
			var totalItems = await repo.GetCountAsync(countSpecification);
			return new PaginatedResult<ProductDto>(queryParams.PageIndex, queryParams.PageSize, totalItems, productDtos);

		}

		public async Task<IEnumerable<TypeDto>> GetAllProductTypesAsync()
		{
			var repo = _unitOfWork.GetRepository<ProductType, int>();
			var types = await repo.GetAllAsync();
			// Mapping logic from ProductType to TypeDto would go here
			var typeDtos = _mapper.Map<IEnumerable<TypeDto>>(types);
			return typeDtos;
		}

		public async Task<ProductDto?> GetProductByIdAsync(int id)
		{
			var specification = new ProductSpecification(id);
			var repo = _unitOfWork.GetRepository<Product, int>();
			var product =  await repo.GetByIdAsync(specification);
			if (product is null) throw new ProductNotFoundException(id);
			// Mapping logic from Product to ProductDto would go here
			var productDto = _mapper.Map<ProductDto>(product);
			return productDto;
		}
	}
}
