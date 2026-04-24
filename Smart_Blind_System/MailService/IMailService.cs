namespace Smart_Blind_System.API.MailService
{
    public interface IMailService
    {
        Task SendEmailAsync(string mailTo, string subject, string body);
    }
}
