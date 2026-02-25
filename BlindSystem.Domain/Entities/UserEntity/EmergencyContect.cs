namespace BlindSystem.Domain.Entities.UserEntity
{
    public class EmergencyContect
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Relation { get; set; }

        public ApplicationUser user { get; set; }




    }
}
