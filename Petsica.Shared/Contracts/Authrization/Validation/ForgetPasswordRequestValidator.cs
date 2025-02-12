using Petsica.Shared.Contracts.Authrization.Request;

namespace Petsica.Shared.Contracts.Authrization.Validation
{
    public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
    {
        public ForgetPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
