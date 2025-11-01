using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Classes
{
	public class PaginatedResult<Dto>
	{
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int Count { get; set; }
		public IEnumerable<Dto> Data { get; set; }
		public PaginatedResult(int pageIndex, int pageSize, int count, IEnumerable<Dto> data)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			Count = count;
			Data = data;
		}
	}
}
