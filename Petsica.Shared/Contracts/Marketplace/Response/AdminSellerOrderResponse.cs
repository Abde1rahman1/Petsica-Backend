using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Response
{
    public record AdminSellerOrderResponse(
    int SellerOrderId,
    int OrderId,
    string UserId,
    string SellerId,
    DateTime CreatedAt,
    bool Status,
    bool IsCancelled,
    int TotalQuantity,
    decimal TotalPrice,
    List<OrderItemResponse> OrderItems
    );

}
