using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class ServiceManager(IUnitOfWork _unitOfWork,
		IMapper _mapper,
		IBasketRepository _basketRepository ,
		UserManager<ApplicationUser> _userManager,
		IConfiguration configuration) : IServiceManager
	{
		private readonly Lazy<IProductService> _productService=new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
		public IProductService ProductService => _productService.Value;

		private readonly Lazy<IBasketService> basketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
		public IBasketService BasketService => basketService.Value;

		private readonly Lazy<IAuthenticationService> _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager,configuration,_mapper));
		public IAuthenticationService AuthenticationService => _authenticationService.Value;
		private readonly Lazy<IOrderService> _orderService = new Lazy<IOrderService>(() => new OrderService( _mapper, _basketRepository, _unitOfWork));
		public IOrderService OrderService => _orderService.Value;
	}
}
