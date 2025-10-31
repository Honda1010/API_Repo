using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController(IServiceManager _serviceManager):ControllerBase
	{
		[HttpGet("types")]
		public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllProductTypesAsync()
		{
			var types = await _serviceManager.ProductService.GetAllProductTypesAsync();
			return Ok(types);
		}
		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllProductBrandsAsync()
		{
			var brands = await _serviceManager.ProductService.GetAllProductBrandsAsync();
			return Ok(brands);
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsAsync()
		{
			var products = await _serviceManager.ProductService.GetAllProductsAsync();
			return Ok(products);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDto?>> GetProductByIdAsync(int id)
		{
			var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
			if (product == null) return NotFound();
			return Ok(product);
		}

	}
}
