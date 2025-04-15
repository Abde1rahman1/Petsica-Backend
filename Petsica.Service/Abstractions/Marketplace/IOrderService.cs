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
        Task<Result<List<OrderResponse>>> CreateOrderFromCartAsync(string userId, string address, CancellationToken cancellationToken);
        Task<Result<List<OrderResponse>>> GetOrdersForUserAsync(string userId, CancellationToken cancellationToken);
        Task<Result<List<OrderResponse>>> GetOrdersForSellerAsync(string sellerId, CancellationToken cancellationToken);
        Task<Result<OrderResponse>> GetOrderByIdForUserAsync(int orderId, string userId, CancellationToken cancellationToken);
        Task<Result<OrderResponse>> GetOrderByIdForSellerAsync(int orderId, string sellerId, CancellationToken cancellationToken);
        Task<Result> MarkOrderAsCompletedAsync(string sellerId, int orderId, CancellationToken cancellationToken);
        Task<Result> CancelOrderByUserAsync(int orderId, string userId, CancellationToken cancellationToken);
        Task<Result<List<OrderResponse>>> GetAllOrdersAsync(CancellationToken cancellationToken);

    }
}
