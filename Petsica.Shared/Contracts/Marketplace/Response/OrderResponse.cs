using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Response
{
    public record OrderResponse(
    int OrderID,
    decimal TotalPrice,
    DateTime CreatedAt,
    bool Status,
    string Address,
    List<OrderItemResponse> OrderItems
    );
}
