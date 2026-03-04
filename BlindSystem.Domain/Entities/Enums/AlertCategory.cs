namespace BlindSystem.Domain.Entities.Enums
{
    public enum AlertCategory
    {
        SOSButton = 1,
        CriticalObstacle = 2, // لما الجسم يقرب جداً (أقل من 50 سم مثلاً)
        FallDetected = 3
    }
}
