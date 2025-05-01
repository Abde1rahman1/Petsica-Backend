using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petsica.Core.Entities.Community;
using Petsica.Service.Abstractions.Dashboard;
using Petsica.Service.Services;
using System.Threading;

namespace Petsica.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DashboardsController(IDashboardService dashboard) : ControllerBase
{
    private readonly IDashboardService _dashboard = dashboard;

    [HttpGet("GetNumberUsers")]
    public async Task<IActionResult> GetNumberUsers( CancellationToken cancellationToken)
    {
        var users = await _dashboard.GetUserCountsByRole( cancellationToken);

        return users.IsSuccess ? Ok(users.Value) : BadRequest();
    }
    [HttpGet("GetGeneralInfo")]
    public async Task<IActionResult> GetGeneralInfo(CancellationToken cancellationToken)
    {
        var users = await _dashboard.GetGeneralInfoAsync(cancellationToken);

        return users.IsSuccess ? Ok(users.Value) : BadRequest();
    }

    [HttpGet("GetNumberOfConfirmidEmail")]
    public async Task<IActionResult> GetNumberOfConfirmidEmail(CancellationToken cancellationToken)
    {
        var users = await _dashboard.GetEmailConfirmationStatsAsync(cancellationToken);

        return users.IsSuccess ? Ok(users.Value) : BadRequest();
    }
    [HttpGet("GetAllTimeUserActivity")]
    public async Task<IActionResult> GetAllTimeUserActivity(CancellationToken cancellationToken)
    {
        int days = 30;
        var users = await _dashboard.GetAllTimeUserActivityAsync( cancellationToken);

        if (users != null && users.Any())
        {
            return Ok(users); // Return the active users list
        }

        return BadRequest("No active users found"); 
    }

    [HttpGet("GetTopContributors")]
    public async Task<IActionResult> GetTopContributors(CancellationToken cancellationToken)
    {
        var topContributors = await _dashboard.GetTopContributorsAsync(cancellationToken);
        return Ok(topContributors);
    }

    [HttpGet("GetTopPosts")]
    public async Task<IActionResult> GetTopPosts(CancellationToken cancellationToken)
    {
        var topContributors = await _dashboard.GetTopPostsAsync(cancellationToken);
        return Ok(topContributors);
    }

}
