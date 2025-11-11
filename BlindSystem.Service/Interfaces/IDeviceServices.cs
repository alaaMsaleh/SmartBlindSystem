using Smart_Blind_System.API.DTOs.DevicesDto;

namespace BlindSystem.Service.Interfaces
{
    public interface IDeviceServices
    {
        Task<IEnumerable<DeviceBatteryDto>> GetBatteryStatusAsync();
    }
}
