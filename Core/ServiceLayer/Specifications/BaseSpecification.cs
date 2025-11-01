using DomainLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Specifications
{
	public abstract class BaseSpecification<Tentity,Tkey>:ISpecification<Tentity,Tkey> where Tentity : class
	{
		protected BaseSpecification(Expression<Func<Tentity,bool>>? _Criteria)
		{
			Criteria = _Criteria;
		}
		public Expression<Func<Tentity, bool>>? Criteria { get; protected set; }
		public List<Expression<Func<Tentity, object>>> Includes { get; } = [];
		public Expression<Func<Tentity, object>> OrderBy { get; protected set; } 
		public Expression<Func<Tentity, object>> OrderByDescending { get; protected set; }
		protected void AddInclude(Expression<Func<Tentity, object>> includeExpression)
		{
			Includes.Add(includeExpression);
		}
		protected void AddOrderBy(Expression<Func<Tentity, object>> orderByExpression)
		{
			OrderBy = orderByExpression;
		}
		protected void AddOrderByDescending(Expression<Func<Tentity, object>> orderByDescExpression)
		{
			OrderByDescending = orderByDescExpression;
		}
		public int Take { get; protected set; }
		public int Skip { get; protected set; }
		public bool IsPagingEnabled { get; protected set; }
		protected void ApplyPaging(int PageSize, int PageIndex)
		{
			IsPagingEnabled = true;
			Skip = PageSize * (PageIndex - 1);
			Take = PageSize;
		}

	}
}
