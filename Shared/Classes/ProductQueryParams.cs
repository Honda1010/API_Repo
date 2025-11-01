using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Classes
{
	public class ProductQueryParams
	{
		public int? BrandId { get; set; }
		public int? TypeId { get; set; }
		public string? Search { get; set; }
		public SortOptions? sortOptions { get; set; }

		private const int MaxPageSize = 10;
		public int PageIndex { get; set; } = 1;	
		private int _pageSize = 5;
		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
		}
		
	}
}
