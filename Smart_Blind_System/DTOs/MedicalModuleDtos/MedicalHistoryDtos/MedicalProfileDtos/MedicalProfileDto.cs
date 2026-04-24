namespace Smart_Blind_System.API.DTOs.MedicalProfileDtos
{
    public class MedicalProfileDto
    {
        public Guid Id { get; set; }
        public string? BloodType { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string? MedicalNotes { get; set; }
        public string? DoctorPhone { get; set; }
        public List<AllergyDto> Allergies { get; set; } = new();
        public List<ChronicDiseaseDto> ChronicDiseases { get; set; } = new();
        public List<MedicalHistoryEntryDto> MedicalHistory { get; set; } = new();
        public List<MedicationDto> Medications { get; set; } = new();
    }


}
