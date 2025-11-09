using DomainLayer.Contracts;
using DomainLayer.Models.BasketModels;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PresistanceLayer.Reposirtory
{
	public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
	{
		public readonly IDatabase _database = connection.GetDatabase();
		public async Task<Basket?> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? TimeToLive = null)
		{
			var basketJson = JsonSerializer.Serialize(basket);
			bool isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, basketJson, TimeToLive ?? TimeSpan.FromDays(30));
			if (isCreatedOrUpdated) return basket;
			else return null;
		}

		public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _database.KeyDeleteAsync(basketId);
		}

		public async Task<Basket?> GetBasketAsync(string basketId)
		{
			var basketJson = await _database.StringGetAsync(basketId);
			if (basketJson.IsNullOrEmpty) return null;
			else return JsonSerializer.Deserialize<Basket>(basketJson!);
		}
	}
}
