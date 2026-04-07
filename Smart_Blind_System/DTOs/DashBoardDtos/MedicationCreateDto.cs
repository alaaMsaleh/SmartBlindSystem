namespace Smart_Blind_System.API.DTOs.DashBoardDtos
{
    public class MedicationCreateDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }

        public List<DateTime> Schedule { get; set; }
    }
}
