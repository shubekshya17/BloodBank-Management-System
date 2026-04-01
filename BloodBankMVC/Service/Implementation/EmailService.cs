using BloodBankMVC.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BloodBankMVC.Service.Implementation
{
    public class EmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_settings.SmtpHost)
            {
                Port = _settings.Port,
                Credentials = new NetworkCredential(_settings.FromEmail, _settings.Password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_settings.FromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
