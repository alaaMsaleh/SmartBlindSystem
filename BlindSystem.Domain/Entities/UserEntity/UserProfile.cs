namespace BlindSystem.Domain.Entities.UserEntity
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public MedicalProfile MedicalProfile { get; set; }

        public List<EmergencyContect> EmergencyContects { get; set; } = new List<EmergencyContect>();

        public List<FaceProfile> faceProfiles { get; set; } = new List<FaceProfile>();


    }
}
