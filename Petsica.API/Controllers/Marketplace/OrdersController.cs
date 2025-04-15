using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petsica.Service.Abstractions.Marketplace;
using Petsica.Service.Services.Marketplace;
using Petsica.Shared.Contracts.Marketplace.Request;
using Petsica.Shared.Extensions;
using Petsica.Shared.Result;

namespace Petsica.API.Controllers.Marketplace
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var result = await _orderService.CreateOrderFromCartAsync(User.GetUserId()!, request.Address, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return result.ToProblem();
        }

        [HttpGet("userorders")]
        public async Task<IActionResult> GetOrdersForUser(CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var result = await _orderService.GetOrdersForUserAsync(User.GetUserId()!, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("sellerorders")]
        public async Task<IActionResult> GetOrdersForSeller(CancellationToken cancellationToken)
        {
            var sellerId = User.GetUserId()!;
            var result = await _orderService.GetOrdersForSellerAsync(sellerId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> GetOrderByIdForUser(int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrderByIdForUserAsync(orderId, User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("seller/{orderId:int}")]
        public async Task<IActionResult> GetOrderByIdForSeller(int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrderByIdForSellerAsync(orderId, User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPut("{orderId}/complete")]
        public async Task<IActionResult> MarkAsCompleted(int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.MarkOrderAsCompletedAsync(User.GetUserId()!, orderId, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelOrderByUser(int id, CancellationToken cancellationToken)
        {
            var result = await _orderService.CancelOrderByUserAsync(id, User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
        {
            var result = await _orderService.GetAllOrdersAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

    }
}
