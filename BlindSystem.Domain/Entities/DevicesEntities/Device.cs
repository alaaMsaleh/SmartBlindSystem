namespace BlindSystem.Domain.Entities.DevicesEntities
{
    public class Device
    {
        public Guid Id { get; set; }
        public Guid OwnerUserId { get; set; }
        public string DeviceName { get; set; } = null!;

        public string SerialNumber { get; set; } = null!;

        public double BatteryLevel { get; set; }

        public enum ConnectionStatus;

        public DateTime LastSync { get; set; }

        public string FrimWareVersion { get; set; } = null!;
    }
}
