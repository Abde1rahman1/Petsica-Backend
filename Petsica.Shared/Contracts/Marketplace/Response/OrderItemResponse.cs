using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Response
{
    public record OrderItemResponse(
    int ProductId,
    string ProductName,
    String Photo,
    int Quantity,
    decimal Price,
    decimal TotalPrice
    );
}
