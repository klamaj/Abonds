using API.Services.Interfaces;
using Core.Models.EmailModels;
using MailKit.Net.Smtp;
using MimeKit;

namespace API.Services.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfigurationModel _emailConfiguration;
        public EmailSender(EmailConfigurationModel emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        // Interface Implementation
        public async Task SendEmailAsync(MessageModel message)
        {
            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }

        // Create Email Message
        private MimeMessage CreateEmailMessage(MessageModel message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Abonds Agency", _emailConfiguration.From!));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        // Send
        private async Task SendAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                try 
                {
                    await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfiguration.Username, _emailConfiguration.Password);

                    await client.SendAsync(message);
                }
                catch
                {
                    // log
                    throw;
                }
                finally 
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}