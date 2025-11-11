namespace BlindSystem.Domain.Entities.UserEntity
{
    public class EmergencyContect
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Relation { get; set; }

        public UserProfile user { get; set; }




    }
}
