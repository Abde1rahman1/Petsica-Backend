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
            var result = await _orderService.CreateOrderFromCartAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? Created() : result.ToProblem();
        }

        [HttpGet("userorders")]
        public async Task<IActionResult> GetOrdersForUser(CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var result = await _orderService.GetOrdersForUserAsync(User.GetUserId()!, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdForUser(int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrderByIdForUserAsync(orderId, User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("sellerorders")]
        public async Task<IActionResult> GetOrdersForSeller(CancellationToken cancellationToken)
        {
            var sellerId = User.GetUserId()!;
            var result = await _orderService.GetOrdersForSellerAsync(sellerId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("seller/{orderId}")]
        public async Task<IActionResult> GetOrderByIdForSeller(int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetSellerOrderByIdAsync(orderId, User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPut("complete/{orderId}")]
        public async Task<IActionResult> MarkAsCompleted(int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.MarkSellerOrderAsCompletedAsync(User.GetUserId()!, orderId, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPut("admin/complete/{orderId}")]
        public async Task<IActionResult> CompleteOrderByAdmin(int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.MarkOrderAsCompletedByAdminAsync(orderId, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelOrderByUser(int id, CancellationToken cancellationToken)
        {
            var result = await _orderService.CancelOrderByUserAsync(id, User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPut("admin/cancel/{orderId}")]
        public async Task<IActionResult> CancelOrderByAdmin(int orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.CancelOrderByAdminAsync(orderId, cancellationToken);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrdersForAdminAsync(CancellationToken cancellationToken)
        {
            var result = await _orderService.GetAllOrdersForAdminAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("all/sellerorders")]
        public async Task<IActionResult> GetAllSellerOrdersForAdmin(CancellationToken cancellationToken)
        {
            var result = await _orderService.GetAllSellerOrdersForAdminAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("admin/seller/{sellerOrderId}")]
        public async Task<IActionResult> GetSellerOrderByIdForAdmin(int sellerOrderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetSellerOrderByIdForAdminAsync( sellerOrderId,cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("admin/{orderId}")]
        public async Task<IActionResult> GetOrderByIdForAdmin(int orderId,CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrderByIdForAdminAsync(orderId,cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }

}
