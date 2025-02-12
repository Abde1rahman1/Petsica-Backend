using Petsica.Shared.Contracts.Users.Request;

namespace Petsica.Shared.Contracts.Users.Validation
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(x => x.Address)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(x => x.Location)
                .NotEmpty()
                .Length(3, 100);
        }
    }
}
