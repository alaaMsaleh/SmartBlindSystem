namespace Smart_Blind_System.API.DTOs.BraceletDtos
{
    public class AlertCreateDto
    {
        public string UserId { get; set; }
        public string DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
        public string AlertType { get; set; } // "SOS" or "Obstacle"
    }
}
