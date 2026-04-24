namespace Smart_Blind_System.API.DTOs.MedicalProfileDtos
{
    public class CreateAllergyDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Severity { get; set; }
        public string? Reaction { get; set; }
    }
}
