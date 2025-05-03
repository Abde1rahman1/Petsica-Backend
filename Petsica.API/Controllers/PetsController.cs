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

        //[HttpPost("AddRemindPet")]
        //public async Task<IActionResult> AddRemindPet([FromBody] AddRemindPetRequest request, CancellationToken cancellationToken)
        //{
        //    var result = await _service.AddRemindPetAsync(User.GetUserId()!, request, cancellationToken);

        //    return result.IsSuccess ? Created() : result.ToProblem();
        //}

        //[HttpPut("UpdateRemindPet/{RemindID}")]
        //public async Task<IActionResult> UpdateRemindPet([FromRoute] int RemindID, [FromBody] UpdateRemindPetRequest request, CancellationToken cancellationToken)
        //{
        //    var result = await _service.UpdateRemindPetAsync(RemindID, request, cancellationToken);

        //    return result.IsSuccess ? Created() : result.ToProblem();
        //}

        //[HttpGet("GetAllRemind")]

        //public async Task<IActionResult> GetAllRemind([FromQuery] int petId, CancellationToken cancellationToken)
        //{
        //    var result = await _service.GetAllRemindAsync(User.GetUserId()!, petId, cancellationToken);

        //    return result.IsSuccess ? Ok(result) : result.ToProblem();
        //}

        [HttpGet("GetAllPets")]

        public async Task<IActionResult> GetAllPets(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllPetsAsync(User.GetUserId()!, cancellationToken);

            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }

        [HttpPost("PetAdoptionToggle/{petId}")]
        public async Task<IActionResult> PetAdoptionToggle([FromRoute] int petId, CancellationToken cancellationToken)
        {
            var result = await _service.PetAdoptionToggle(User.GetUserId()!, petId, cancellationToken);

            return result.IsSuccess ? Created() : result.ToProblem();

        }
        [HttpPost("PetMatingToggle/{petId}")]
        public async Task<IActionResult> PetMatingToggle([FromRoute] int petId, CancellationToken cancellationToken)
        {
            var result = await _service.PetMatingToggle(User.GetUserId()!, petId, cancellationToken);

            return result.IsSuccess ? Ok() : result.ToProblem();

        }

        [HttpGet("GetPetProfil/{petId}")]

        public async Task<IActionResult> GetPetProfil([FromRoute] int petId, CancellationToken cancellationToken)
        {
            var result = await _service.GetPetProfil(User.GetUserId()!, petId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();

        }

        [HttpGet("GetPetMatingList")]

        public async Task<IActionResult> GetPetMatingList(CancellationToken cancellationToken)
        {
            var result = await _service.GetPetMatingList(cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();

        }

        [HttpGet("GetPetAdoptionList")]

        public async Task<IActionResult> GetPetAdoptionList(CancellationToken cancellationToken)
        {
            var result = await _service.GetPetAdoptionList(cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();

        }
    }
}
