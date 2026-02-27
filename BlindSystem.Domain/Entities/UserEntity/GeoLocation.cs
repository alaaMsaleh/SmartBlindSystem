using Microsoft.EntityFrameworkCore;

namespace BlindSystem.Domain.Entities.UserEntity
{
    [Owned]
    public class GeoLocation
    {
        public double Latitude { get; set; } //if user at house or mall 

        public double Longitude { get; set; } = 0;

        public string? ReadableAddress { get; set; }

        public DateTime CapturedAt { get; set; }

        public double? Accuracy { get; set; }
    }

}
