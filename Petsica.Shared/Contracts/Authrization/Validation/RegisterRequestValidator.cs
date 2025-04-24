using Petsica.Core.Const;
using Petsica.Shared.Const;
using Petsica.Shared.Contracts.Authrization.Request;
namespace Petsica.Shared.Contracts.Authrization.Validation
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(RegexPatterns.Password)
                .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");


            RuleFor(x => x.Type)
                .NotEmpty()
               .Must(name => RoleName.GetAllRoleNames().Contains(name));



        }
    }
}
