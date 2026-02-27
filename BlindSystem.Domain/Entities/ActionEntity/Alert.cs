using BlindSystem.Domain.Entities.DevicesEntities;
using BlindSystem.Domain.Entities.UserEntity;

namespace BlindSystem.Domain.Entities.ActionEntity
{
    public class Alert
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid DeviceId { get; set; }
        public string AlertType { get; set; } = string.Empty;
        public string Description { get; set; }
        //public enum Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsResolved { get; set; }
        public bool Notification { get; set; }

        public GeoLocation Location { get; set; }

        public virtual ApplicationUser user { get; set; }

        public virtual Device Device { get; set; }

        //public virtual EmergencyContect EmergancyContacts { get; set; } => not need Emergancy here becouse there relation between Alert and User
    }
}
