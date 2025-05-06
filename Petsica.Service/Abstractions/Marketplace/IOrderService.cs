using Petsica.Shared.Contracts.Marketplace.Request;
using Petsica.Shared.Contracts.Marketplace.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Abstractions.Marketplace
{
    public interface IOrderService
    {
        Task<Result<List<OrderResponse>>> CreateOrderFromCartAsync(string userId, CreateOrderRequest request, CancellationToken cancellationToken);
        Task<Result<List<OrderResponse>>> GetOrdersForUserAsync(string userId, CancellationToken cancellationToken);
        Task<Result<OrderResponse>> GetOrderByIdForUserAsync(int orderId, string userId, CancellationToken cancellationToken);
        Task<Result<List<SellerOrderResponse>>> GetOrdersForSellerAsync(string sellerId, CancellationToken cancellationToken);
        Task<Result<SellerOrderResponse>> GetSellerOrderByIdAsync(int sellerOrderId, string sellerId, CancellationToken cancellationToken);
        Task<Result> MarkSellerOrderAsCompletedAsync(string sellerId, int sellerOrderId, CancellationToken cancellationToken);
        Task<Result> MarkOrderAsCompletedByAdminAsync(int orderId, CancellationToken cancellationToken);
        Task<Result> CancelOrderByUserAsync(int orderId, string userId, CancellationToken cancellationToken);
        Task<Result> CancelOrderByAdminAsync(int orderId, CancellationToken cancellationToken);
        Task<Result<List<AdminOrderResponse>>> GetAllOrdersForAdminAsync(CancellationToken cancellationToken);
        Task<Result<List<AdminSellerOrderResponse>>> GetAllSellerOrdersForAdminAsync(CancellationToken cancellationToken);
        Task<Result<AdminSellerOrderResponse>> GetSellerOrderByIdForAdminAsync(int sellerOrderId, CancellationToken cancellationToken);
        Task<Result<AdminOrderResponse>> GetOrderByIdForAdminAsync(int orderId, CancellationToken cancellationToken);

    }
}
