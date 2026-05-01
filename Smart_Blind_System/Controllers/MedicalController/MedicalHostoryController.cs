using AutoMapper;
using BlindSystem.Domain.Entities.MedicalEntity;
using BlindSystem.Domain.Service_Contract.MedicalProfileInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Blind_System.API.DTOs.MedicalProfileDtos;

namespace Smart_Blind_System.API.Controllers.MedicalController
{
  
    public class MedicalHostoryController : BaseController
    {

        private readonly IMedicalProfileService _medicalProfileService;
        private readonly IMapper _mapper;

        public MedicalHostoryController(IMedicalProfileService medicalProfileService, IMapper mapper) 
        {

            _medicalProfileService = medicalProfileService;
            _mapper = mapper;

        }



        [HttpPost("medical-history")]
        public async Task<ActionResult<MedicalHistoryEntryDto>> CreateMedicalHistoryEntry([FromBody] CreateMedicalHistoryEntryDto dto)
        {
            var userId = GetUserId();
            var entry = _mapper.Map<MedicalHistoryEntry>(dto);
            var result = await _medicalProfileService.CreateMedicalHistoryEntry(userId, entry);
            if (result is null) return BadRequest("Create medical profile first.");
            return Ok(_mapper.Map<MedicalHistoryEntryDto>(result));
        }

        [HttpGet("Get-medical-history")]
        public async Task<ActionResult<IEnumerable<MedicalHistoryEntryDto>>> GetMedicalHistory()
        {
            var userId = GetUserId();

            var entries = await _medicalProfileService.GetMedicalHistoryEntries(userId);

            return Ok(_mapper.Map<IEnumerable<MedicalHistoryEntryDto>>(entries));
        }

        [HttpPut("medical-history/{id:guid}")]
        public async Task<ActionResult<MedicalHistoryEntryDto>> UpdateMedicalHistoryEntry(Guid id, [FromBody] UpdateMedicalHistoryEntryDto dto)
        {
            var updated = _mapper.Map<MedicalHistoryEntry>(dto);
            var result = await _medicalProfileService.UpdateMedicalHistoryEntry(GetUserId(), id, updated);

            if (result is null) return NotFound("Medical history entry not found.");

            return Ok(_mapper.Map<MedicalHistoryEntryDto>(result));
        }

        [HttpDelete("medical-history/{id:guid}")]
        public async Task<ActionResult> DeleteMedicalHistoryEntry(Guid id)
        {
            var success = await _medicalProfileService.DeleteMedicalHistoryEntry(GetUserId(), id);

            if (!success) return NotFound("Medical history entry not found.");

            return NoContent();
        }

    }
}
