using Petsica.Core.Entities.Community;
using Petsica.Core.Entities.Users;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community;
using Petsica.Shared.Contracts.Users.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Petsica.Service.Service.Community;
public class LikeService(ApplicationDbContext context) : ILikeService
{
	private readonly ApplicationDbContext _context = context;

	public async Task<Result<LikeResponse>> AddAsync(int postId,string user, CancellationToken cancellationToken)
	{

		var check = await _context.UserLikePosts.FirstOrDefaultAsync(u=>u.UserID==user && u.PostID==postId ,cancellationToken );
		//
		//
		// check here 

		var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostID == postId, cancellationToken);
		var like = new UserLikePost
		{ 
			UserID = user ,
			PostID=postId
		};
		await _context.UserLikePosts.AddAsync(like, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		
		var response = like.Adapt<LikeResponse>();
		return Result.Success(response);
	}

	public async Task<Result> DeleteLikeAsync(int postId, string user, CancellationToken cancellationToken)
	{
		
		var like = await _context.UserLikePosts
			.FirstOrDefaultAsync(u => u.UserID == user && u.PostID == postId, cancellationToken);

		_context.UserLikePosts.Remove(like);
		await _context.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}

	public async Task<Result<List<UsersLikePostResponse>>> GetUsersWhoLikedPostAsync(int postId, CancellationToken cancellationToken)
	{
		
		var post = await _context.Posts
			.FirstOrDefaultAsync(p => p.PostID == postId, cancellationToken);

	
		var users = await _context.UserLikePosts
			.Where(like => like.PostID == postId)
			.Select(like => like.User) 
			.ToListAsync(cancellationToken);

		var response = new List<UsersLikePostResponse>();

		foreach (var user in users)
		{
			var userResponse = new UsersLikePostResponse
			(
				UserId: user.UserID
			);

			response.Add(userResponse);
		}


		return Result.Success(response);
	}
}
