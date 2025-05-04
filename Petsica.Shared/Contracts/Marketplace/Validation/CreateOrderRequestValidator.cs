using Petsica.Shared.Contracts.Marketplace.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Validation
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(500).WithMessage("Address cannot be more than 500 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Length(11).WithMessage("Phone number must be exactly 11 digits.")
                .Matches(@"^\d{11}$").WithMessage("Phone number must consist of only numbers.");
        }
    }
}
