using AutoMapper;
using BlindSystem.Domain.Entities.MedicalEntity;
using BlindSystem.Domain.Service_Contract.MedicalProfileInterface;
using Microsoft.AspNetCore.Mvc;
using Smart_Blind_System.API.DTOs.MedicalModuleDtos.MedicalProfileDtos;
using Smart_Blind_System.API.DTOs.MedicalProfileDtos;

namespace Smart_Blind_System.API.Controllers.MedicalController
{
    public class MedicationsController : BaseController
    {

        private readonly IMedicalProfileService _medicalProfileService;
        private readonly IMapper _mapper;
        public MedicationsController(IMedicalProfileService medicalProfileService, IMapper mapper)
        {
            _medicalProfileService = medicalProfileService;
            _mapper = mapper;

        }


        /////Medications Endpoint

        [HttpPost("medications")]
        public async Task<ActionResult<MedicationDto>> CreateMedication([FromBody] CreateMedicationDto dto)
        {
            var userId = GetUserId();
            var medication = _mapper.Map<Medication>(dto);
            var result = await _medicalProfileService.CreateMedication(userId, medication);
            if (result is null) return BadRequest("Create medical profile first.");
            return Ok(_mapper.Map<MedicationDto>(result));
        }

        [HttpGet("Get-medications")]
        public async Task<ActionResult<IEnumerable<MedicationDto>>> GetMedications()
        {
            var userId = GetUserId();

            var medications = await _medicalProfileService.GetMedications(userId);

            return Ok(_mapper.Map<IEnumerable<MedicationDto>>(medications));
        }

        [HttpPut("medications/{id:guid}")]
        public async Task<ActionResult<MedicationDto>> UpdateMedication(Guid id, [FromBody] UpdateMedicationDto dto)
        {
            var updated = _mapper.Map<Medication>(dto);
            var result = await _medicalProfileService.UpdateMedication(GetUserId(), id, updated);

            if (result is null) return NotFound("Medication not found.");

            return Ok(_mapper.Map<MedicationDto>(result));
        }

        [HttpDelete("medications/{id:guid}")]
        public async Task<ActionResult> DeleteMedication(Guid id)
        {
            var success = await _medicalProfileService.DeleteMedication(GetUserId(), id);

            if (!success) return NotFound("Medication not found.");

            return NoContent();
        }

    }
}
