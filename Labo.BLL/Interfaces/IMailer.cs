using System.Net.Mail;

namespace Labo.BLL.Interfaces
{
    public interface IMailer
    {
        Task SendAsync(string subject, string message, Attachment attachment, params string[] to);
        Task SendAsync(string subject, string message, params string[] to);
    }
}