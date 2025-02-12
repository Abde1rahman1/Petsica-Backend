using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Petsica.Core.Entities.Community;
using Petsica.Core.Entities.Users;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community;
using Petsica.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Community;
public class PostService(
	ApplicationDbContext context,
	UserManager<ApplicationUser> userManager,
	   IHttpContextAccessor httpContextAccessor) : IPostService
{
	private readonly ApplicationDbContext _context = context;
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
	private readonly UserManager<ApplicationUser> _userManager;

	public async Task<Result<PostResponse>> AddAsync(PostRequest request, CancellationToken cancellationToken = default)
	{
		var post = request.Adapt<Post>();
		

		await _context.AddAsync(post, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		return Result.Success(post.Adapt<PostResponse>());
	}
}
