using BlindSystem.Domain.Entities.ActionEntity;
using BlindSystem.Infrastructure.Data.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Blind_System.API.DTOs.BraceletDtos;

namespace Smart_Blind_System.API.Controllers.BraceletControllers
{
    public class AlertsController : BaseController
    {

        private readonly BlindSystemDbContext _dbContext;
        public AlertsController(BlindSystemDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        [HttpPost("send-sos")]

        public async Task<ActionResult> SendSos([FromForm] AlertCreateDto dto)
        {
            Guid userGuid = Guid.Parse(dto.UserId);
            Guid DeviceGuid = Guid.Parse(dto.DeviceId);
            var alter = new Alert
            {
                UserId = userGuid,
                DeviceId = DeviceGuid,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                AlertType = dto.AlertType,
                CreatedAt = DateTime.UtcNow

            };

            await _dbContext.Alerts.AddAsync(alter);
            await _dbContext.SaveChangesAsync();

            var contacts = await _dbContext.EmergencyContect
                .Where(u => u.UserId == userGuid)
                .ToListAsync();

            return Ok(new
            {
                Message = "SOS Received and Logged",
                ContactCount = contacts.Count
            });

        }
    }
}
