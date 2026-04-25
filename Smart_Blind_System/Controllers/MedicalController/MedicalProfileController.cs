using AutoMapper;
using BlindSystem.Domain.Entities.MedicalEntities;
using BlindSystem.Domain.Entities.MedicalEntity;
using BlindSystem.Domain.Service_Contract.MedicalProfileInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Blind_System.API.Controllers;
using Smart_Blind_System.API.DTOs.MedicalProfileDtos;

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




}