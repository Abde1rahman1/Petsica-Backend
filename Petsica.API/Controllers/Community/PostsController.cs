using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petsica.Core.Entities.Community;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community;
namespace Petsica.API.Controllers.Community;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostsController(IPostService postService) : ControllerBase
{
	private readonly IPostService _postService = postService;

	[HttpPost("")]
	public async Task<IActionResult> Add([FromBody] PostRequest request,CancellationToken cancellationToken)
	{
		var result = await _postService.AddAsync(request, cancellationToken);

		return result.IsSuccess ? Ok(result) : BadRequest(result);
	}
}
