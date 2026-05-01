using AutoMapper;
using BlindSystem.Domain.Entities.MedicalEntities;
using BlindSystem.Domain.Entities.MedicalEntity;
using BlindSystem.Domain.Service_Contract.MedicalProfileInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Blind_System.API.Controllers;
using Smart_Blind_System.API.DTOs.MedicalModuleDtos.MedicalProfileDtos;
using Smart_Blind_System.API.DTOs.MedicalProfileDtos;
using Smart_Blind_System.API.DTOs.MedicalProfileDtos.ChronicDiseaseiesDtos;

[Authorize]
public class MedicalProfileController : BaseController
{
    private readonly IMedicalProfileService _medicalProfileService;
    private readonly IMapper _mapper;


    public MedicalProfileController(IMedicalProfileService medicalProfileService, IMapper mapper)
    {
        _medicalProfileService = medicalProfileService;
        _mapper = mapper;
    }

    [HttpPost("Create_MedicalProfile")]
    public async Task<ActionResult> CreateNewMedicalProfileAsync([FromForm] CreateMedicalProfileDto dto)
    {

        var userId = GetUserId();

        var medicalProfile = _mapper.Map<MedicalProfile>(dto);


        var result = await _medicalProfileService.CreateMedicalProfile(userId, medicalProfile);

        if (result == null) return BadRequest("Could not create profile or it already exists.");

        return Ok(new { Message = "MedicalProfile added successfully" });
    }

    [HttpGet("get-profile")]
    public async Task<ActionResult> GetFullProfile()
    {
        var userId = GetUserId();

        var profile = await _medicalProfileService.GetFullProfileAsync(userId);


        if (profile is null)
            return NotFound("No medical profile found. Create one first.");


        return Ok(_mapper.Map<MedicalProfileDto>(profile));
    }

    [HttpPut("update-profile")]
    public async Task<ActionResult> UpdateMedicalProfile([FromForm] UpdateMedicalProfileDto dto)
    {
        var userId = GetUserId();

        var medicalProfile = _mapper.Map<MedicalProfile>(dto);

        medicalProfile.UserId = userId;


        var updateprfile = await _medicalProfileService.UpdateMedicalProfile(userId, medicalProfile);
        if (updateprfile == null) return NotFound("Not There Data ");

        return Ok(_mapper.Map<MedicalProfileDto>(updateprfile));
    }


    /////Allergies Endpoint ********** let's go LOLO

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


    /////MedicalHistory Endpoint

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