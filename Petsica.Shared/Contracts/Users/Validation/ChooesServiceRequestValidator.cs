using Petsica.Shared.Contracts.Users.Request;

namespace Petsica.Shared.Contracts.Users.Validation
{
    public class ChooesServiceRequestValidator : AbstractValidator<ChooesServiceRequest>
    {
        public ChooesServiceRequestValidator()
        {
            RuleFor(x => x.ServiceID)
                .NotEmpty();

        }
    }
}
