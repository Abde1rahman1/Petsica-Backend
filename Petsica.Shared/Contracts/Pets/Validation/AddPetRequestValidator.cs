using Petsica.Shared.Contracts.Pets.Request;

namespace Petsica.Shared.Contracts.Users.Validation
{
    public class AddPetRequestValidator : AbstractValidator<AddPetRequest>
    {
        public AddPetRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Species)
                .NotEmpty();
            RuleFor(x => x.Breed)
                .NotEmpty();
            RuleFor(x => x.Gender)
                .NotEmpty();

        }
    }
}
