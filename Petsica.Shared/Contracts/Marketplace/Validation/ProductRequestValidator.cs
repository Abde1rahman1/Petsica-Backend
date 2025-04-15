using Petsica.Shared.Contracts.Marketplace.Request;

namespace Petsica.Shared.Contracts.Marketplace.Validation
{
	public class ProductRequestValidator : AbstractValidator<ProductRequest>
    {
		public ProductRequestValidator()
		{
			RuleFor(p=>p.Description)
				.NotEmpty()
				.MaximumLength(300);
			RuleFor(p => p.ProductName)
				.NotEmpty();
			RuleFor(p => p.Price)
				.NotEmpty();
			RuleFor(p=>p.Photo)
				.NotEmpty();
		}

	}
}
