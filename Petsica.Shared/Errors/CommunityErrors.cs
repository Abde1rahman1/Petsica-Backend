using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Error;
public static class CommunityErrors
{

	public static readonly Errors UnauthorizedToUpdatePost =
			new("Post.UnauthorizedToUpdatePost", "Unauthorized to update this post !", StatusCodes.Status401Unauthorized);

	public static readonly Errors InvalidPostId =
			new("Post.InvalidPostId", "Invalid Post Id", StatusCodes.Status404NotFound);
}
