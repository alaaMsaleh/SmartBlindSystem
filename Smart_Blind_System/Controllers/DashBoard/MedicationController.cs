using BlindSystem.Domain.Entities.MedicalEntity;
using BlindSystem.Infrastructure.Data.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Blind_System.API.DTOs.IdentityUser;
using Smart_Blind_System.API.DTOs.MedicalProfileDtos;
using System.Security.Claims;

namespace Smart_Blind_System.API.Controllers.DashBoard
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private readonly BlindSystemDbContext _dbContext;
        public MedicationController(BlindSystemDbContext dbContext)
        {
            _dbContext = dbContext;

        }


        [HttpGet("Medical-Profile")]
        public async Task<ActionResult<IReadOnlyList<GetProfileSummaryDto>>

        [HttpPost("AddNewMedication")]
        public async Task<ActionResult> CreateNewMedicateAsync([FromForm] MedicationCreateDto dto)
        {

            if (!ModelState.IsValid) return BadRequest();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Unauthorized("User not found");

            var NewMedication = new Medication
            {
                Name = dto.Name,
                Dosage = dto.Dosage,
                Schedule = dto.Schedule

            };

            var result = await _dbContext.Set<Medication>().AddAsync(NewMedication);
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Medication added successfully",
                MedicationName = NewMedication.Name
            });

        }

        [Authorize]
        [HttpGet("GetSpecificMeical")]
        public async Task<ActionResult<IReadOnlyList<Medication>>> GetMedicationAsync(string Name)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var SearchMedicate = await _dbContext.Set<Medication>().FindAsync(x => x.Name == Name);

            if (SearchMedicate == null) return NotFound(new { Message = "This Medication Not Found In Db" });

            return Ok(SearchMedicate);

        }

        [Authorize]
        [HttpPost("Emergancy-Contect")]
        public async Task<ActionResult> AddUserEnergacyContectAsync(EmergancyContectDto dto)
        {

        }


    }
}
