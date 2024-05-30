using API.Models;

namespace API.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MessageModel message);
    }
}