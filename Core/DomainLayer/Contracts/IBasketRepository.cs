using DomainLayer.Models.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
	public interface IBasketRepository
	{
		Task<Basket?> GetBasketAsync(string basketId);
		Task<Basket?> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? TimeToLive=null);
		Task<bool> DeleteBasketAsync(string basketId);
	}
}
