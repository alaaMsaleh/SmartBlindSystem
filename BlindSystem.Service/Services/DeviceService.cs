using BlindSystem.Infrastructure.Data.DBContext;
using BlindSystem.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Smart_Blind_System.API.DTOs.DevicesDto;

namespace BlindSystem.Service.Services
{
    public class DeviceService : IDeviceServices
    {
        private readonly BlindSystemDbContext _context;

        public DeviceService(BlindSystemDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeviceBatteryDto>> GetBatteryStatusAsync()
        {
            return await _context.Devices
                .Select(d => new DeviceBatteryDto
                {
                    DeviceName = d.DeviceName,
                    BatteryLevel = d.BatteryLevel,
                    LastUpdate = d.LastSync,
                })
                .ToListAsync();
        }
    }
}
