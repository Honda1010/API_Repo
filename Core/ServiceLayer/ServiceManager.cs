using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository) : IServiceManager
	{
		private readonly Lazy<IProductService> _productService=new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
		public IProductService ProductService => _productService.Value;

		private readonly Lazy<IBasketService> basketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
		public IBasketService BasketService => basketService.Value;
	}
}
