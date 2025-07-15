using Microsoft.Extensions.Options;
using Repository.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Service.Utils
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings _settings;

        public EmailSender(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            try
            {
                using var smtpClient = new SmtpClient(_settings.Host)
                {
                    Port = _settings.Port,
                    Credentials = new NetworkCredential(_settings.FromEmail, _settings.Password),
                    EnableSsl = _settings.EnableSsl
                };

                var mail = new MailMessage
                {
                    From = new MailAddress(_settings.FromEmail, "Smart Shopping Team"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };

                mail.To.Add(to);

                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("שגיאה בשליחת מייל: " + ex.Message);
                throw new Exception("שגיאה בשליחת מייל: " + ex.Message);
            }
        }

    }
}
