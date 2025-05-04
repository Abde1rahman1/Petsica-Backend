using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Request
{
    public record CreateOrderRequest(
    string Address,
    string PhoneNumber
    );
}
