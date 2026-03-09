namespace Smart_Blind_System.API.DTOs.MedicalProfileDtos
{
    public class MedicalHistoryEntryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string? DoctorName { get; set; }
    }
}