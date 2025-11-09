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
	public class ProductSpecification:BaseSpecification<Product,int>
	{
		public ProductSpecification(ProductQueryParams queryParams)
			:base((p)=>(!queryParams.BrandId.HasValue || p.BrandId==queryParams.BrandId)
					&&(!queryParams.TypeId.HasValue || p.TypeId==queryParams.TypeId)
			&&(string.IsNullOrWhiteSpace(queryParams.Search)||p.Name.ToLower().Contains(queryParams.Search.ToLower())))
		{
			AddInclude(p => p.ProductBrand);
			AddInclude(p => p.ProductType);
			ApplyPaging(queryParams.PageSize, queryParams.PageIndex);

			switch (queryParams.sortOptions) {
				case SortOptions.NameAsc:
					AddOrderBy(p => p.Name);
					break;
				case SortOptions.NameDesc:
					AddOrderByDescending(p => p.Name);
					break;
				case SortOptions.PriceAsc:
					AddOrderBy(p => p.Price);
					break;
				case SortOptions.PriceDesc:
					AddOrderByDescending(p => p.Price);
					break;
				default:
					break;
			}
		}
		public ProductSpecification(int id):base(p=>p.Id==id)
		{
			AddInclude(p => p.ProductBrand);
			AddInclude(p => p.ProductType);
		}
	}
}
