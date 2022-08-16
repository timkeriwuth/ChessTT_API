using System.Net.Mail;

namespace Labo.IL.Services
{
    public interface IMailer
    {
        void Send(string subject, string message, Attachment attachment, params string[] to);
        void Send(string subject, string message, params string[] to);
    }
}