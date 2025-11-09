using Shared.Dtos.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
	public interface IBasketService
	{
		Task<BasketDto?> GetBasketAsync(string basketId);
		Task<BasketDto?> CreateOrUpdateBasketAsync(BasketDto basketDto, TimeSpan? TimeToLive = null);
		Task<bool> DeleteBasketAsync(string basketId);
	}
}
