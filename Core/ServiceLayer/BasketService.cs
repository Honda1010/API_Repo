using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using ServiceAbstractionLayer;
using Shared.Dtos.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class BasketService(IBasketRepository _basketRepository,IMapper _mapper):IBasketService
	{

		public async Task<BasketDto?> GetBasketAsync(string basketId)
		{
			var basket = await _basketRepository.GetBasketAsync(basketId);
			if (basket is null) throw new BasketNotFoundException(basketId);
			var basketDto = _mapper.Map<BasketDto>(basket);
			return basketDto;
		}
		public async Task<BasketDto?> CreateOrUpdateBasketAsync(BasketDto basketDto, TimeSpan? TimeToLive = null)
		{
			var basket = _mapper.Map<DomainLayer.Models.BasketModels.Basket>(basketDto);
			var createdOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(basket, TimeToLive);
			if (createdOrUpdatedBasket is null) throw new Exception("there something wrong happen when create basket");
			var createdOrUpdatedBasketDto = _mapper.Map<BasketDto>(createdOrUpdatedBasket);
			return createdOrUpdatedBasketDto;
		}
		public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _basketRepository.DeleteBasketAsync(basketId);
		}


	}
}
