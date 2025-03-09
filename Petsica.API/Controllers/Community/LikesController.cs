using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petsica.Service.Abstractions.Community;
using Petsica.Service.Service.Community;
using Petsica.Shared.Contracts.Community;
using Petsica.Shared.Extensions;
using System.Security.Claims;
using Petsica.Shared.Result;
using Microsoft.AspNetCore.Authorization;

namespace Petsica.API.Controllers.Community;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LikesController(ILikeService likeService) : ControllerBase
{
	private readonly ILikeService _likeService = likeService;

	[HttpPost("{PostId}")]
	public async Task<IActionResult> AddLike(int PostId, CancellationToken cancellationToken)
	{
		var result = await _likeService.AddAsync( PostId, User.GetUserId()!, cancellationToken);
		return result.IsSuccess ? Created() : result.ToProblem();
	}

	[HttpDelete("{postId}")]
	public async Task<IActionResult> DeleteLike(int postId, CancellationToken cancellationToken)
	{
		var result = await _likeService.DeleteLikeAsync(postId, User.GetUserId()!, cancellationToken);

		return result.IsSuccess ? Ok() : BadRequest(result.Error);
	}

	[HttpGet("{postId}")]
	public async Task<IActionResult> GetUsersWhoLikedPost(int postId, CancellationToken cancellationToken)
	{
		var result = await _likeService.GetUsersWhoLikedPostAsync(postId, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
	}
}
