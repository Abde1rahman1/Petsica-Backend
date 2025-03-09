using Petsica.Shared.Contracts.Pets.Request;

namespace Petsica.Shared.Contracts.Users.Validation
{
    public class AddRemindPetRequestValidator : AbstractValidator<AddRemindPetRequest>
    {
        public AddRemindPetRequestValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty();
            RuleFor(x => x.Date)
                .NotEmpty();


        }
    }
}
