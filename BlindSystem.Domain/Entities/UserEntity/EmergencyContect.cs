namespace BlindSystem.Domain.Entities.UserEntity
{
    public class EmergencyContect : BaseEntity
    {

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Relation { get; set; }

        public ApplicationUser user { get; set; }




    }
}
