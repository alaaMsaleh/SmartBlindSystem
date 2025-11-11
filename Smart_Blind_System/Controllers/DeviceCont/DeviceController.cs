using BlindSystem.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
//using BlindSystem.Service.Interfaces;

namespace Smart_Blind_System.API.Controllers.DeviceCont
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceServices _deviceServices;

        public DeviceController(IDeviceServices deviceServices)
        {
            _deviceServices = deviceServices;
        }

        [HttpGet("battery-status")]
        public async Task<IActionResult> GetBatteryStatus()
        {
            var batteryData = await _deviceServices.GetBatteryStatusAsync();
            return Ok(batteryData);
        }
    }
}
