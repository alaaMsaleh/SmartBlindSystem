namespace Smart_Blind_System.API.DTOs.MedicalProfileDtos
{
    public class ChronicDiseaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DiagnosedDate { get; set; }
    }
}