using Shared.Dtos;
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

		Task<IEnumerable<ProductDto>> GetAllProductsAsync();
		Task<ProductDto?> GetProductByIdAsync(int id);

	}
}
