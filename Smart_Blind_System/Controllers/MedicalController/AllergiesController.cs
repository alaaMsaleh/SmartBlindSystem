using AutoMapper;
using BlindSystem.Domain.Entities.MedicalEntities;
using BlindSystem.Domain.Service_Contract.MedicalProfileInterface;
using Microsoft.AspNetCore.Mvc;
using Smart_Blind_System.API.DTOs.MedicalProfileDtos;

namespace Smart_Blind_System.API.Controllers.MedicalController
{

    public class AllergiesController : BaseController
    {
        private readonly IMedicalProfileService _medicalProfileService;
        private readonly IMapper _mapper;


        public AllergiesController(IMedicalProfileService medicalProfileService, IMapper mapper)
        {


            _medicalProfileService = medicalProfileService;
            _mapper = mapper;

        }

        [HttpPost("allergies")]
        public async Task<ActionResult<AllergyDto>> CreateNewAllergy([FromBody] CreateAllergyDto dto)
        {
            var userId = GetUserId();
            var allergy = _mapper.Map<Allergy>(dto);
            var result = await _medicalProfileService.CreateNewAllergy(userId, allergy);
            if (result is null) return BadRequest("Create medical profile first.");
            return Ok(_mapper.Map<AllergyDto>(result));
        }

        [HttpGet("Get-allergies")]
        public async Task<ActionResult<IEnumerable<Allergy>>> GetNewAllergy([FromQuery] string? severity = null)
        {
            var userId = GetUserId();

            var allergies = await _medicalProfileService.GetAllergies(userId, severity);

            return Ok(_mapper.Map<IEnumerable<AllergyDto>>(allergies));
        }



        [HttpPut("allergies/{id:guid}")]
        public async Task<ActionResult<AllergyDto>> UpdateAllergy(Guid id, [FromBody] UpdateAllergyDto dto)
        {
            var updated = _mapper.Map<Allergy>(dto);
            var result = await _medicalProfileService.UpdateAllergy(GetUserId(), id, updated);

            if (result is null) return NotFound("Allergy not found.");

            return Ok(_mapper.Map<AllergyDto>(result));
        }


        [HttpDelete("allergies/{id:guid}")]
        public async Task<ActionResult> DeleteAllergy(Guid id)
        {
            var success = await _medicalProfileService.DeleteAllergy(GetUserId(), id);

            if (!success) return NotFound("Allergy not found.");

            return NoContent();
        }

    }
}
