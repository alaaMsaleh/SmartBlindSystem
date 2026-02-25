namespace BlindSystem.Domain.Entities.UserEntity
{
    public class FaceProfile
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string TypeRelationShip { get; set; }

        public string ImageUrl { get; set; }
        public string image { get; set; }

        public string EmbeddingHash { get; set; }

        public DateTime AddedAt { get; set; }

        public ApplicationUser User { get; set; }
    }
}
