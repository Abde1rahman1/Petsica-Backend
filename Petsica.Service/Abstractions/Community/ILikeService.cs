using Petsica.Shared.Contracts.Community.Response;
using Petsica.Shared.Contracts.Users.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Abstractions.Community;
public interface ILikeService
{
	Task<Result<LikeResponse>> AddAsync(int postId, string user, CancellationToken cancellationToken = default);
	Task<Result> DeleteLikeAsync(int postId, string user, CancellationToken cancellationToken = default);
	Task<Result<List<UsersLikePostResponse>>> GetUsersWhoLikedPostAsync(int postId, CancellationToken cancellationToken = default);
}
