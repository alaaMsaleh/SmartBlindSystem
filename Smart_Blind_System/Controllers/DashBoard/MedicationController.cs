using BlindSystem.Domain.Entities.UserEntity;
using BlindSystem.Infrastructure.Data.DBContext;
using Microsoft.AspNetCore.Mvc;
using Smart_Blind_System.API.DTOs.DashBoardDtos;

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

        [HttpPost("AddNewMedication")]
        public async Task<ActionResult> CreateNewMedicate(MedicationCreateDto dto)
        {

            if (!ModelState.IsValid) return BadRequest();

            var NewMedication = new Medication
            {
                Name = dto.Name,
                Dosage = dto.Dosage,
                Schedule = dto.Schedule

            };

            var result = await _dbContext.Set<Medication>().AddAsync(NewMedication);

            return Ok(result);





        }

    }
}
