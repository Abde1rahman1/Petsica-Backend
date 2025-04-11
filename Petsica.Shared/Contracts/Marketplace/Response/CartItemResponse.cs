using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Response
{
    public record CartItemResponse(
    int ProductId,
    string ProductName,
    string Photo,
    decimal Price,
    decimal Discount,
    int Quantity,
    bool IsAvailable,
    decimal SubTotal
    );

}
