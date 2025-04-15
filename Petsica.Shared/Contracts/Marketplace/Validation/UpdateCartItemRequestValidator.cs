using Petsica.Shared.Contracts.Marketplace.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Validation
{
    public class UpdateCartItemRequestValidator : AbstractValidator<UpdateCartItemRequest>
    {
        public UpdateCartItemRequestValidator()
        {
            RuleFor(r => r.ProductId).NotEmpty();
            RuleFor(r => r.Quantity).GreaterThan(0);
        }
    }
}
