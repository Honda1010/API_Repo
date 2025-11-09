using Shared.Classes;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
	public interface IProductService
	{
		Task<IEnumerable<TypeDto>> GetAllProductTypesAsync();
		Task<IEnumerable<BrandDto>> GetAllProductBrandsAsync();

		Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
		Task<ProductDto?> GetProductByIdAsync(int id);

	}
}
