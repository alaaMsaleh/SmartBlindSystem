
using MailKit.Net.Smtp;
using MimeKit;

namespace Smart_Blind_System.API.MailService
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _config;

        public MailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string mailTo, string subject, string body)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_config["MailSettings:SenderEmail"]);
            email.To.Add(MailboxAddress.Parse(mailTo));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["MailSettings:Server"],
                                    int.Parse(_config["MailSettings:Port"]),
                                    MailKit.Security.SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(_config["MailSettings:SenderEmail"],
                                         _config["MailSettings:Password"]);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
