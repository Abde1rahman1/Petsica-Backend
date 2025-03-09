using Petsica.Shared.Contracts.Users.Request;
using Petsica.Shared.Contracts.Users.Response;

namespace Petsica.Shared.Contracts.Users.Validation
{
    public class ServiceRequestValidator : AbstractValidator<ServiceRequest>
    {
        public ServiceRequestValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.Price)
                .NotEmpty();

            RuleFor(x => x.Location)
                .NotEmpty();
        }
    }
}
