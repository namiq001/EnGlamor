using EnGlamor.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using EnGlamor.Models;
using EnGlamor.ViewModels.MailSenderVM;

namespace EnGlamor.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSetting _emailSettings;
        private readonly SmtpClient _smtpClient;

        public EmailService(IOptions<MailSetting> emailSettings)
        {

            _emailSettings = emailSettings.Value;
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password)
            };
            _smtpClient = smtpClient;
        }

        public void SendMessage(MailRequestVM mailRequest)
        {
            MailMessage newMessage = new MailMessage(_emailSettings.Email, mailRequest.ToEmail)
            {
                Subject = mailRequest.Subject,
                Body = mailRequest.Body,
                IsBodyHtml = true
            };
            _smtpClient.Send(newMessage);
        }
    }
}
