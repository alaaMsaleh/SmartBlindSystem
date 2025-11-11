using BlindSystem.Domain.Entities.DevicesEntities;

namespace BlindSystem.Domain.Interfaces.IDevicesRepo
{
    public interface IDevicesRepository
    {
        Task<IEnumerable<Device>> GetAllAsync();
        Task<Device> GetByIdAsync(Guid id);

        Task UpdateAsync(Device device);

    }
}
