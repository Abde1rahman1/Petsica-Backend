using Petsica.Shared.Contracts.Marketplace.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Validation
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Category name is required.");
        }
    }
}
