using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community.Request;
using Petsica.Shared.Extensions;

namespace Petsica.API.Controllers.Community
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserFollowsController(IUserFollow userFollow) : ControllerBase
    {
		private readonly IUserFollow _userFollow = userFollow;

		[HttpPost("follow/{followedUserId}")]
        public async Task<IActionResult> FollowUser(string followedUserId, CancellationToken cancellationToken = default)
        {
			var result = await _userFollow.FollowUser(User.GetUserId()!, followedUserId, cancellationToken);
			return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
		}
		[HttpDelete("follow/{followedUserId}")]
		public async Task<IActionResult> DeleteFollow(string followedUserId, CancellationToken cancellationToken = default)
		{
			var result = await _userFollow.DeleteFollow(User.GetUserId()!, followedUserId, cancellationToken);
			return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
		}

		[HttpGet("followers/{followedUserId}")]
		public async Task<IActionResult> GetAllFollowers(string followedUserId, CancellationToken cancellationToken = default)
		{
			var result = await _userFollow.GetAllFollowers(followedUserId, cancellationToken);
			return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
		}

		[HttpGet("following/{UserId}")]
		public async Task<IActionResult> GetAllFollowing(string UserId, CancellationToken cancellationToken = default)
		{
			var result = await _userFollow.GetAllFollowing(UserId, cancellationToken);
			return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
		}
	}
}
