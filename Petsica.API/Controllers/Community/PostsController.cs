using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petsica.Core.Entities.Community;
using Petsica.Infrastructure;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community;
using System.Security.Claims;
namespace Petsica.API.Controllers.Community;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostsController(ApplicationDbContext context, IPostService postService) : ControllerBase
{
	private readonly IPostService _postService = postService;
	private readonly ApplicationDbContext _context = context;
	[HttpPost("")]
	public async Task<IActionResult> Add(CancellationToken cancellationToken)
	{
		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		if (userId == null)
		{
			return Unauthorized("User not found");
		}
		var currentUser = userId;

		

		var post = new Post
		{
			Content = "This is a sample post.",
			Date = DateTime.UtcNow,
			Photo = "https://example.com/photo.jpg",
			UserID = currentUser
		};

		await _context.Posts.AddAsync(post);
		await _context.SaveChangesAsync();
		//await _context.Posts.AddAsync(newPost, cancellationToken);
		//await _context.SaveChangesAsync(cancellationToken);
		//var result = await _postService.AddAsync(userId, request, cancellationToken);

		return Ok(post);
	}

	[HttpGet]
	public async Task<IActionResult> GetAllPosts(CancellationToken cancellationToken)
	{
		var result = await _postService.GetAllPostsAsync(cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
	}
}
