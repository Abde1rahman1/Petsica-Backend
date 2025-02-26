using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Petsica.Core.Entities.Community;
using Petsica.Core.Entities.Users;
using Petsica.Service.Abstractions;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community;
using Petsica.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Petsica.Service.Service.Community;
public class PostService(
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
       IHttpContextAccessor httpContextAccessor) : IPostService
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager;


	public async Task<Result<PostResponse>> AddAsync(string userId, PostRequest request, CancellationToken cancellationToken = default)
	{

		var currentUser = userId;

		var newPost = new Post
		{
			Content = request.Content,
			UserID = userId,
		};

		await _context.Posts.AddAsync(newPost, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		
		var response = newPost.Adapt<PostResponse>();

		return Result.Success(response);
	}

	public async Task<Result<List<PostResponse>>> GetAllPostsAsync(CancellationToken cancellationToken = default)
	{
		
			
			var posts = await _context.Posts
				.Include(p => p.User) 
				.ToListAsync(cancellationToken);

			
			var response = posts.Adapt<List<PostResponse>>();

			return Result.Success(response);
		
		
	}

}
