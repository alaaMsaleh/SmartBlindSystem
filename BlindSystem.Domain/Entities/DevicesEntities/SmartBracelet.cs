using BlindSystem.Domain.Entities.Enums;

namespace BlindSystem.Domain.Entities.DevicesEntities
{
    public class SmartBracelet : Device
    {
        public BodyLocation bodyLocation { get; set; }
        public string? FirmwareVersion { get; set; } // معلومة تقنية مفيدة للصيانة
    }

}
