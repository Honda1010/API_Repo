using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
	public interface ISpecification<Tentity,TKey> where Tentity : class
	{
		Expression<Func<Tentity, bool>>? Criteria { get;  }
		List<Expression<Func<Tentity, object>>> Includes { get; }
		Expression<Func<Tentity,object>> OrderBy { get; }
		Expression<Func<Tentity,object>> OrderByDescending { get; }
		int Take { get; }
		int Skip { get; }
		bool IsPagingEnabled { get; }
	}
}
