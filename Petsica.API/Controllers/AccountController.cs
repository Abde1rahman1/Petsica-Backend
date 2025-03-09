using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petsica.Service.Abstractions.Users;
using Petsica.Shared.Contracts.Users.Request;
using Petsica.Shared.Extensions;
using Petsica.Shared.Result;

namespace Petsica.API.Controllers
{
    [Route("me")]
    [ApiController]
    [Authorize]
    public class AccountController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("")]

        public async Task<IActionResult> Info()
        {
            var result = await _userService.GetProfileAsync(User.GetUserId()!);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPut("info")]
        public async Task<IActionResult> Info([FromBody] UpdateProfileRequest request)
        {
            var result = await _userService.UpdateProfileAsync(User.GetUserId()!, request);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = await _userService.ChangePasswordAsync(User.GetUserId()!, request);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }



        [HttpPost("AddService")]
        public async Task<IActionResult> AddService([FromBody] ServiceRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.AddService(User.GetUserId()!, request, cancellationToken);


            return result.IsSuccess ? Created() : result.ToProblem();

        }

        [HttpGet("AllService")]
        public async Task<IActionResult> AllService(CancellationToken cancellationToken)
        {
            var result = await _userService.GetServicesAsync(cancellationToken);


            return result.IsSuccess ? Ok(result) : result.ToProblem();

        }

        [HttpPost("ChooesService")]
        public async Task<IActionResult> ChooesService([FromBody] ChooesServiceRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.ChooesServiceAsync(User.GetUserId()!, request, cancellationToken);


            return result.IsSuccess ? Created() : result.ToProblem();

        }
    }
}
