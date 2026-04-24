namespace Smart_Blind_System.API.DTOs.MedicalProfileDtos
{
    public class MedicationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public List<DateTime> Schedule { get; set; } = new();
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }




}
