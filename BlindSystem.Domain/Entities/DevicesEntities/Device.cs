using BlindSystem.Domain.Entities.ActionEntity;
using BlindSystem.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlindSystem.Domain.Entities.DevicesEntities
{
    public class Device : BaseEntity
    {


        public DeviceType Type { get; set; }
        public string DeviceName { get; set; } = null!;

        public string SerialNumber { get; set; } = null!;

        public double BatteryLevel { get; set; }

        public enum ConnectionStatus;

        public DateTime LastSync { get; set; }

        public string FrimWareVersion { get; set; } = null!;
        public Guid OwnerUserId { get; set; }
        [ForeignKey("OwnerUserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    }
}
