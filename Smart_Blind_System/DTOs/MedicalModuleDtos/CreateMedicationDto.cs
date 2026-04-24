namespace Smart_Blind_System.API.DTOs.MedicalProfileDtos
{
    public class CreateMedicationDto
    {


        public string Name { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public List<DateTime> Schedule { get; set; } = new();
    }
}
