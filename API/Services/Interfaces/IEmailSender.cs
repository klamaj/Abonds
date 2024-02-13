using Core.Models.EmailModels;

namespace API.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MessageModel message);
    }
}