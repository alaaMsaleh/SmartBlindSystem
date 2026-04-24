using System.ComponentModel;

namespace BlindSystem.Domain.Entities.Enums
{
    public enum BloodType
    {
        [Description("A+")]
        APositive = 1,

        [Description("A-")]
        ANegative,

        [Description("B+")]
        BPositive,

        [Description("B-")]
        BNegative,

        [Description("AB+")]
        ABPositive,

        [Description("AB-")]
        ABNegative,

        [Description("O+")]
        OPositive,

        [Description("O-")]
        ONegative
    }
}
