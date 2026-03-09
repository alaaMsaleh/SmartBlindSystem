namespace Smart_Blind_System.API.DTOs.MedicalProfileDtos
{
    public class MedicationCreateDto
    {

        public string Name { get; set; }
        public string Dosage { get; set; }

        public List<DateTime> Schedule { get; set; }
    }
}
