using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petsica.Service.Abstractions.Pets;
using Petsica.Shared.Contracts.Pets.Request;
using Petsica.Shared.Extensions;
using Petsica.Shared.Result;

namespace Petsica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PetsController(IPetsService service) : ControllerBase
    {
        private readonly IPetsService _service = service;

        [HttpPost("AddPet")]
        public async Task<IActionResult> AddPet([FromBody] AddPetRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.AddPetAsync(User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Created() : result.ToProblem();
        }

        [HttpPost("AddRemindPet")]
        public async Task<IActionResult> AddRemindPet([FromBody] AddRemindPetRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.AddRemindPetAsync(User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Created() : result.ToProblem();
        }

        [HttpPut("UpdateRemindPet")]
        public async Task<IActionResult> UpdateRemindPet(int RemindID, [FromBody] UpdateRemindPetRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateRemindPetAsync(RemindID, request, cancellationToken);

            return result.IsSuccess ? Created() : result.ToProblem();
        }

        [HttpGet("GetAllRemind")]

        public async Task<IActionResult> GetAllRemind([FromQuery] int petId, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllRemindAsync(User.GetUserId()!, petId, cancellationToken);

            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }

        [HttpGet("GetAllPets")]

        public async Task<IActionResult> GetAllPets(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllPetsAsync(User.GetUserId()!, cancellationToken);

            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }

        [HttpPost("PetAdoptionOn")]
        public async Task<IActionResult> PetAdoptionOn(AddPetServiceRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.PetAdoptionOn(User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Created() : result.ToProblem();

        }
        [HttpPost("PetAdoptionOff")]
        public async Task<IActionResult> PetAdoptionOff(AddPetServiceRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.PetAdoptionOff(User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Ok() : result.ToProblem();

        }

        [HttpPost("PetMatingOn")]
        public async Task<IActionResult> PetMatingOn(AddPetServiceRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.PetMatingOn(User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Ok() : result.ToProblem();

        }

        [HttpPost("PetMatingOff")]

        public async Task<IActionResult> PetMatingOff(AddPetServiceRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.PetMatingOff(User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Ok() : result.ToProblem();

        }

        [HttpGet("GetPetService")]

        public async Task<IActionResult> GetPetService(int petId, CancellationToken cancellationToken)
        {
            var result = await _service.GetPetServices(User.GetUserId()!, petId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();

        }
    }
}
