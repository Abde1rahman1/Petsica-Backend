using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Community;
public class PostRequestValidetor : AbstractValidator<PostRequest>
{
	public PostRequestValidetor()
	{
		RuleFor(x => x.Content)
			.NotEmpty().WithMessage("Content is required.")
			.MaximumLength(300).WithMessage("Content must not exceed 300 characters.");

		RuleFor(x => x.Date)
			.NotEmpty().WithMessage("Date is required.");
	}
}

