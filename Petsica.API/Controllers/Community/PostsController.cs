using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community;
using Petsica.Shared.Contracts.Community.Request;
using Petsica.Shared.Extensions;
using Petsica.Shared.Result;
using System.Security.Claims;
namespace Petsica.API.Controllers.Community;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostsController(IPostService postService) : ControllerBase
{
    private readonly IPostService _postService = postService;
    [HttpPost("")]
    public async Task<IActionResult> Add(PostRequest postRequest, CancellationToken cancellationToken)
    {
        var result = await _postService.AddAsync(User.GetUserId()!, postRequest, cancellationToken);
        return result.IsSuccess ? Created() : result.ToProblem();
    }

    [HttpPut("{postId}")]
    public async Task<IActionResult> Update(int PostId, [FromBody] PostRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _postService.UpdatePostById(User.GetUserId()!, PostId, request, cancellationToken);
        return result.IsSuccess ? Created() : result.ToProblem();
	}


    [HttpGet]
    public async Task<IActionResult> GetAllPosts(CancellationToken cancellationToken)
    {
        var result = await _postService.GetAllPostsAsync(cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet("{postId}")]
	public async Task<IActionResult> GetPostById(int postId,CancellationToken cancellationToken)
	{
		var result = await _postService.GetPostById(postId, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost("delete/{PostId}")]

    public async Task<IActionResult> DeleteById(int PostId, CancellationToken cancellationToken)
    {
        var result = await _postService.DeleteById(User.GetUserId()!, PostId, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
	}

    [HttpGet("myPosts")]
    public async Task<IActionResult> GetMyPosts(CancellationToken cancellationToken)
    {
        var result = await _postService.GetMyPostsAsync(User.GetUserId()!, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
