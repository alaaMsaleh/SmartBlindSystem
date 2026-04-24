namespace Smart_Blind_System.API.DTOs.MedicalProfileDtos
{
    // Kept for backward compatibility - use MedicalProfileSummaryDto
    public class MedicalProfileSummaryDto
    {
        public Guid Id { get; set; }
        public string? BloodType { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public int AllergyCount { get; set; }
        public int DiseaseCount { get; set; }
        public int MedicationCount { get; set; }
        public int HistoryCount { get; set; }
    }
}
