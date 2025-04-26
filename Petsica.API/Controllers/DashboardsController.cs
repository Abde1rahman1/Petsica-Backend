using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petsica.Service.Abstractions.Dashboard;

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
        var users = await _dashboard.GetNumberOfConfirmidEmailAsync(cancellationToken);

        return users.IsSuccess ? Ok(users.Value) : BadRequest();
    }
}
