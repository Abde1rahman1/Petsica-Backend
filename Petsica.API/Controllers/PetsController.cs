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

        [HttpPut("UpdatePet")]

        public async Task<IActionResult> UpdatePet(UpdatePetRequest request)
        {
            var result = await _service.UpdatePet(User.GetUserId()!, request);

            return result.IsSuccess ? Ok() : result.ToProblem();

        }


        [HttpDelete("DeletePet/{petId}")]

        public async Task<IActionResult> UpdatePet([FromRoute] int petId)
        {
            var result = await _service.DeletePet(User.GetUserId()!, petId);

            return result.IsSuccess ? Ok() : result.ToProblem();

        }
    }
}
