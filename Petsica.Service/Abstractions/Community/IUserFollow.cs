using Petsica.Shared.Contracts.Community.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Abstractions.Community
{
	public interface IUserFollow
	{
		Task<Result<UserFollowResponse>> FollowUser(string userId, string followedUserId, CancellationToken cancellationToken = default);

		Task<Result<UserFollowResponse>> DeleteFollow(string userId, string followedUserId, CancellationToken cancellationToken = default);

		Task<Result<List<UserFollowResponse>>> GetAllFollowers(string userId, CancellationToken cancellationToken = default);

		Task<Result<List<GetAllFollowing>>> GetAllFollowing(string userId, CancellationToken cancellationToken = default);
	}
}
