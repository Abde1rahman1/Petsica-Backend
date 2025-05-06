using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Response
{
    public record AdminOrderResponse (
        int OrderID,
        string UserId,
        int TotalQuantity,
        decimal TotalPrice,
        DateTime CreatedAt,
        bool Status,
        string Address,
        string PhoneNumber,
        List<OrderItemResponse> OrderItems
    );
}
