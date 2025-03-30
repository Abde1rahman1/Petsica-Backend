using Petsica.Core.Entities.Community;
using Petsica.Core.Entities.Users;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community.Request;
using Petsica.Shared.Contracts.Community.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Petsica.Service.Services.Community;
public class UserFollowService(ApplicationDbContext context) : IUserFollow
{
	private readonly ApplicationDbContext _context = context;

	public async Task<Result<UserFollowResponse>> FollowUser(string userId, string followedUserId, CancellationToken cancellationToken = default) 
	{
		var existingFollow = await _context.UserFollows
				.FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FollowedUserId == followedUserId);

		// Check if User Follow this before or not 

		var userFollow = new UserFollow
		{
			UserId = userId,
			FollowedUserId = followedUserId,
		};

		_context.UserFollows.Add(userFollow);
		await _context.SaveChangesAsync(cancellationToken);

		var response = new UserFollowResponse(
			UserId: userId,
			FollowedId: followedUserId
			);

		return Result.Success(response);
	}

	public async Task<Result<UserFollowResponse>> DeleteFollow(string userId, string followedUserId, CancellationToken cancellationToken = default)
	{
		var existingFollow = await _context.UserFollows
			.FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FollowedUserId == followedUserId);

		// Check if User Follow this before or not 

		_context.UserFollows.Remove(existingFollow);
		_context.SaveChanges();
		var response = new UserFollowResponse(
			UserId: userId,
			FollowedId: followedUserId
			);
		return Result.Success(response);
	}
	public async Task<Result<List<UserFollowResponse>>> GetAllFollowers(string userId, CancellationToken cancellationToken = default)
	{
		var followerList = await _context.UserFollows
				.Where(u => u.UserId== userId)
				.Select(u => new 
				{
					UserId= u.UserId,
					FollowedId= u.FollowedUserId
				})
				.ToListAsync(cancellationToken);


		var response = new List<UserFollowResponse>();

		foreach (var follower in followerList)
		{
			var followersResponse = new UserFollowResponse
			(
				UserId : follower.UserId,
				FollowedId : follower.FollowedId
			);

			response.Add(followersResponse);
		}

		return Result.Success(response);


	}

	public async Task<Result<List<GetAllFollowing>>> GetAllFollowing(string userId, CancellationToken cancellationToken = default)
	{
		var followingList = await _context.UserFollows
				.Where(u => u.UserId == userId)
				.Select(u => new
				{
					FollowedId = u.FollowedUserId
				})
				.ToListAsync(cancellationToken);


		var response = new List<GetAllFollowing>();

		foreach (var following in followingList)
		{
			var followingResponse = new GetAllFollowing
			(
				FollowingUser: following.FollowedId

			);

			response.Add(followingResponse);
		}

		return Result.Success(response);


	}
}
