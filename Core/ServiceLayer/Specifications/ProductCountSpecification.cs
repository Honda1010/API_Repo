using DomainLayer.Models.ProductModels;
using Shared.Classes;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Specifications
{
	public class ProductCountSpecification:BaseSpecification<Product,int>
	{
		public ProductCountSpecification(ProductQueryParams queryParams)
			: base((p) => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
					&& (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
			&& (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower())))
		{

		}

	}
}
