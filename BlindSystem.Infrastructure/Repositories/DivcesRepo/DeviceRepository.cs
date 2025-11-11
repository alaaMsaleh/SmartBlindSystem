using BlindSystem.Domain.Entities.DevicesEntities;
using BlindSystem.Domain.Interfaces.IDevicesRepo;
using BlindSystem.Infrastructure.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace BlindSystem.Infrastructure.Repositories.DivcesRepo
{
    public class DeviceRepository : IDevicesRepository
    {
        private readonly BlindSystemDbContext _BlindDbContext;
        public DeviceRepository(BlindSystemDbContext blindSystemDbContext)
        {
            _BlindDbContext = blindSystemDbContext;
        }
        public async Task<IEnumerable<Device>> GetAllAsync()

          => await _BlindDbContext.Devices.ToListAsync();


        public async Task<Device?> GetByIdAsync(Guid id)
        {
            return await _BlindDbContext.Devices.FindAsync(id);
        }

        public async Task UpdateAsync(Device device)
        {
            _BlindDbContext.Devices.Update(device);
            await _BlindDbContext.SaveChangesAsync();
        }
    }
}
