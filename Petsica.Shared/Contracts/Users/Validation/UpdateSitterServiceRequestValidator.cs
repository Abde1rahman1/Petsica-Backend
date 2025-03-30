using Petsica.Shared.Contracts.Users.Request;

namespace Petsica.Shared.Contracts.Users.Validation
{
    public class UpdateSitterServiceRequestValidator : AbstractValidator<UpdateSitterServiceRequest>
    {
        public UpdateSitterServiceRequestValidator()
        {
            RuleFor(x => x.ServiceID)
                .NotEmpty();

        }
    }
}
