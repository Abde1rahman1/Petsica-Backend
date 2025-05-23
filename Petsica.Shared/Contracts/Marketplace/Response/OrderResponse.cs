﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Response
{
    public record OrderResponse(
    int OrderID,
    string UserId,
    decimal TotalPrice,
    int TotalQuantity,
    DateTime CreatedAt,
    bool Status,
    string PhoneNumber,
    string Address,
    List<OrderItemResponse> OrderItems
    );
}
