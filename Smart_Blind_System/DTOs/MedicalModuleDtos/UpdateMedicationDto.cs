namespace Smart_Blind_System.API.DTOs.MedicalModuleDtos.MedicalProfileDtos
{
    public class UpdateMedicationDto
    {
        public string? Name { get; set; }
        public string? Dosage { get; set; }
        public List<DateTime>? Schedule { get; set; }
        public bool? IsActive { get; set; }
    }
}
