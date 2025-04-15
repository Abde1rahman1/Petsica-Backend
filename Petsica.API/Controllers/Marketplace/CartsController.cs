using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petsica.Service.Abstractions.Marketplace;
using Petsica.Shared.Contracts.Marketplace.Request;
using Petsica.Shared.Extensions;
using Petsica.Shared.Result;

namespace Petsica.API.Controllers.Marketplace
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController(ICartService cartService) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartRequest request, CancellationToken cancellationToken)
        {
            var result = await _cartService.AddToCartAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? Created() : result.ToProblem();
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetCartItems(CancellationToken cancellationToken)
        {
            var result = await _cartService.GetCartItemsAsync(User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemRequest request, CancellationToken cancellationToken)
        {
            var result = await _cartService.UpdateCartItemAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveItem(int productId, CancellationToken cancellationToken)
        {
            var result = await _cartService.RemoveFromCartAsync(User.GetUserId()!, productId, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
        {
            var result = await _cartService.ClearCartAsync(User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }
    }

}
