using BlindSystem.Domain.Entities.Enums;

namespace Smart_Blind_System.API.DTOs.MedicalProfileDtos
{
    public class UpdateMedicalProfileDto
    {

        public BloodType? BloodType { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string? Notes { get; set; }
        public string? DrPhone { get; set; }
        public Gender? Gender { get; set; }
    }
}
