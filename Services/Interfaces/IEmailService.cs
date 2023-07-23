using EnGlamor.ViewModels.MailSenderVM;

namespace EnGlamor.Services.Interfaces;

public interface IEmailService
{
    void SendMessage(MailRequestVM mailRequest);
}
