using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Response
{
    public record CartResponse(
    List<CartItemResponse> Items,
    int TotalQuantity,
    decimal TotalPrice
    );

}
