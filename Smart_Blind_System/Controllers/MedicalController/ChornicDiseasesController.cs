using AutoMapper;
using BlindSystem.Domain.Entities.MedicalEntities;
using BlindSystem.Domain.Service_Contract.MedicalProfileInterface;
using Microsoft.AspNetCore.Mvc;
using Smart_Blind_System.API.DTOs.MedicalProfileDtos;
using Smart_Blind_System.API.DTOs.MedicalProfileDtos.ChronicDiseaseiesDtos;

namespace Smart_Blind_System.API.Controllers.MedicalController
{
    public class ChornicDiseasesController : BaseController
    {
        private readonly IMedicalProfileService _medicalProfileService;
        private readonly IMapper _mapper;

        public ChornicDiseasesController(IMedicalProfileService medicalProfileService, IMapper mapper)
        {
            _medicalProfileService = medicalProfileService;
            _mapper = mapper;
        }

        /////ChronicDiseases Endpoint

        [HttpPost("chronic-diseases")]
        public async Task<ActionResult<ChronicDiseaseDto>> CreateChronicDisease([FromBody] CreateChronicDiseaseDto dto)
        {
            var userId = GetUserId();
            var chronicDisease = _mapper.Map<ChronicDisease>(dto);
            var result = await _medicalProfileService.CreateChronicDisease(userId, chronicDisease);
            if (result is null) return BadRequest("Create medical profile first.");
            return Ok(_mapper.Map<ChronicDiseaseDto>(result));
        }

        [HttpGet("Get-chronic-diseases")]
        public async Task<ActionResult<IEnumerable<ChronicDiseaseDto>>> GetChronicDiseases()
        {
            var userId = GetUserId();

            var diseases = await _medicalProfileService.GetChronicDiseases(userId);

            return Ok(_mapper.Map<IEnumerable<ChronicDiseaseDto>>(diseases));
        }

        [HttpPut("chronic-diseases/{id:guid}")]
        public async Task<ActionResult<ChronicDiseaseDto>> UpdateChronicDisease(Guid id, [FromBody] UpdateChronicDiseaseDto dto)
        {
            var updated = _mapper.Map<ChronicDisease>(dto);
            var result = await _medicalProfileService.UpdateChronicDisease(GetUserId(), id, updated);

            if (result is null) return NotFound("Chronic disease not found.");

            return Ok(_mapper.Map<ChronicDiseaseDto>(result));
        }

        [HttpDelete("chronic-diseases/{id:guid}")]
        public async Task<ActionResult> DeleteChronicDisease(Guid id)
        {
            var success = await _medicalProfileService.DeleteChronicDisease(GetUserId(), id);

            if (!success) return NotFound("Chronic disease not found.");

            return NoContent();
        }



    }
}
