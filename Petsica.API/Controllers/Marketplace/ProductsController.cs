using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petsica.Core.Entities.Marketplace;
using Petsica.Service.Abstractions.Marketplace;
using Petsica.Shared.Contracts.Community.Request;
using Petsica.Shared.Contracts.Marketplace.Request;
using Petsica.Shared.Extensions;
using Petsica.Shared.Result;


namespace Petsica.API.Controllers.Marketplace
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProductsController(IProductService productService) : ControllerBase
	{
		private readonly IProductService _productService = productService;

		[HttpPost("addProduct")]
		public async Task<IActionResult> Add(ProductRequest productRequest, CancellationToken cancellationToken)
		{
			var result = await _productService.AddAsync(User.GetUserId()!, productRequest, cancellationToken);
			return result.IsSuccess ? Created() : result.ToProblem();
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
		{
			var result = await _productService.GetAllProductsAsync(cancellationToken);

			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}

		[HttpGet("Seller/products")]
		public async Task<IActionResult> GetSellerProducts(CancellationToken cancellationToken)
		{
			var result = await _productService.GetMyProductsAsync(User.GetUserId()!, cancellationToken);

			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}

		[HttpGet("{category}")]
		public async Task<IActionResult> GetAllProducts(string category,CancellationToken cancellationToken)
		{
			var result = await _productService.GetProductsByCategoryAsync(category, cancellationToken);

			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}
		[HttpGet("details/{productId}")]
		public async Task<IActionResult> GetProductById(int productId, CancellationToken cancellationToken)
		{
			var result = await _productService.GetProductByIdAsync(productId, cancellationToken);

			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}

		[HttpPut("edit/{productId}")]
		public async Task<IActionResult> Update(int productId, UpdateProductRequest productRequest, CancellationToken cancellationToken = default)
		{
			var result = await _productService.UpdateProductById(User.GetUserId()!, productId, productRequest, cancellationToken);
			return result.IsSuccess ? NoContent() : result.ToProblem();
		}

		[HttpPost("delete/{productId}")]

		public async Task<IActionResult> DeleteById(int productId, CancellationToken cancellationToken)
		{
			var result = await _productService.DeleteById(User.GetUserId()!, productId, cancellationToken);
			return result.IsSuccess ? NoContent() : BadRequest(result.Error);
		}

		[HttpPost("soldout/{productId}")]

		public async Task<IActionResult> ProductSoldout(int productId, CancellationToken cancellationToken)
		{
			var result = await _productService.ProductSoldoutAsync(User.GetUserId()!, productId, cancellationToken);
			return result.IsSuccess ? NoContent() : BadRequest(result.Error);
		}
	}
}
