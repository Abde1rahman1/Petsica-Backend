using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Response
{
    public record SellerOrderResponse(
    int SellerOrderId,
    string SellerId,
    int OrderId,
    DateTime CreatedAt,
    int TotalQuantity,
    decimal TotalPrice,
    bool Status,
    bool IsCancelled,
    List<OrderItemResponse> OrderItems
    );

}
