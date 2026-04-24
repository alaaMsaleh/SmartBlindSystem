using AutoMapper;
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

        if (profile == null) return NotFound("No profile found for this user.");

        return Ok(_mapper.Map<MedicalProfileDto>(profile));
    }

    [HttpGet("GetSpecificMedical")]
    public async Task<ActionResult> GetMedicationAsync(string name)
    {

        return Ok("Logic should be in Service");
    }
}