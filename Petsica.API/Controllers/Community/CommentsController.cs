using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petsica.Core.Entities.Community;
using Petsica.Service.Abstractions.Community;
using Petsica.Service.Service.Community;
using Petsica.Shared.Contracts.Community.Request;
using Petsica.Shared.Extensions;

namespace Petsica.API.Controllers.Community
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class CommentsController(ICommentService commentService) : ControllerBase
	{
		private readonly ICommentService _commentService = commentService;

		[HttpPost("{PostID}")]
		public async Task<IActionResult> Add(int PostID,CommentRequest commentRequest, CancellationToken cancellationToken)
		{

			var result = await _commentService.AddAsync(User.GetUserId()!,PostID, commentRequest, cancellationToken);
			return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
		}
		
		[HttpGet("{PostId}")]
		public async Task<IActionResult> GetCommentsByPostId(int PostId , CancellationToken cancellationToken)
		{

			var result = await _commentService.GetCommentsByPostId(PostId, cancellationToken);
			return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
		}

		[HttpPut("{CommentId}")]
		public async Task<IActionResult> Update(int CommentId, [FromBody] CommentRequest request, CancellationToken cancellationToken = default)
		{
			var result = await _commentService.UpdatePostById(CommentId, request, cancellationToken);
			return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
		}

		[HttpPost("delete/{CommentId}")]

		public async Task<IActionResult> DeleteById(int CommentId, CancellationToken cancellationToken)
		{
			var result = await _commentService.DeleteById(CommentId, cancellationToken);
			return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
		}
	}
}
