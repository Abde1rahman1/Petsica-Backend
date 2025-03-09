using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community;
using Petsica.Shared.Extensions;
using Petsica.Shared.Result;
namespace Petsica.API.Controllers.Community;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostsController(/*ApplicationDbContext context,*/ IPostService postService) : ControllerBase
{
    private readonly IPostService _postService = postService;
    //	private readonly ApplicationDbContext _context = context;
    [HttpPost("")]
    public async Task<IActionResult> Add(PostRequest postRequest, CancellationToken cancellationToken)
    {
        var result = await _postService.AddAsync(User.GetUserId()!, postRequest, cancellationToken);
        return result.IsSuccess ? Created() : result.ToProblem();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts(CancellationToken cancellationToken)
    {
        var result = await _postService.GetAllPostsAsync(cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
