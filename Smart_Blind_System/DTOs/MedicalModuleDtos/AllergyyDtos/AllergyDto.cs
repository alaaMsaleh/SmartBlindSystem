namespace Smart_Blind_System.API.DTOs.MedicalProfileDtos
{
    public class AllergyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Severity { get; set; }
        public string? Reaction { get; set; }
    }




}
