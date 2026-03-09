namespace BlindSystem.Domain.Entities.UserEntity
{
    public class MedicalProfile : BaseEntity
    {


        public string BoodType { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;



        public string Notes { get; set; } = string.Empty;

        public string DrPhone { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } //Navigation Property

        public ICollection<Medication> Medications { get; set; } = new List<Medication>();
    }
}
