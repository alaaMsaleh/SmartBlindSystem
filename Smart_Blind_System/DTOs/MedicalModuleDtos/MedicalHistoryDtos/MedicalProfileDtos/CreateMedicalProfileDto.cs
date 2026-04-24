namespace Smart_Blind_System.API.DTOs.MedicalProfileDtos
{
    public class CreateMedicalProfileDto
    {
        public int BloodType { get; set; } 

        public int Gender {  get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string? MedicalNotes { get; set; }
        public string? DoctorPhone { get; set; }
    }
}
