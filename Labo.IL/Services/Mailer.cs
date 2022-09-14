using Labo.BLL.Interfaces;
using Labo.IL.Configurations;
using System.Net;
using System.Net.Mail;

namespace Labo.IL.Services
{
    public class Mailer : IMailer
    {
        private readonly MailerConfig _config;
        private readonly SmtpClient _smtpClient;

        public Mailer(MailerConfig config, SmtpClient smtpClient)
        {
            _config = config;
            _smtpClient = smtpClient;
            _smtpClient.Host = _config.Host;
            _smtpClient.Port = _config.Port;
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential(_config.Username, _config.Password);
        }

        public async Task SendAsync(string subject, string message, params string[] to)
        {
            using MailMessage mail = CreateMail(subject, message, to);
            await _smtpClient.SendMailAsync(mail);
        }

        public async Task SendAsync(string subject, string message, Attachment attachment, params string[] to)
        {
            using MailMessage mail = CreateMail(subject, message, to);
            mail.Attachments.Add(attachment);
            await _smtpClient.SendMailAsync(mail);
        }

        private MailMessage CreateMail(string subject, string message, string[] to)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new MailAddress(_config.Username);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
#if DEBUG
            mailMessage.To.Add(_config.TestMail);
#else
            foreach (string dest in to)
            {
                mailMessage.To.Add(dest);
            }
#endif
            return mailMessage;
        }
    }
}
