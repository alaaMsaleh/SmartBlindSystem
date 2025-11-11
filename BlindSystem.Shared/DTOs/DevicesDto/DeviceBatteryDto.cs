namespace Smart_Blind_System.API.DTOs.DevicesDto
{
    public class DeviceBatteryDto
    {
        public string DeviceName { get; set; } = null!;

        public double BatteryLevel { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
