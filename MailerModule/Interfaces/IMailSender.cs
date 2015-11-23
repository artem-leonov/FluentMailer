using System.Collections.Generic;

namespace MailerModule.Interfaces
{
    public interface IMailSender
    {
        IMailSender WithReceiver(string email);
        IMailSender WithReceivers(IEnumerable<string> emails);
        IMailSender WithSubject(string subject);
        void Send();
        void SendAsync();
    }
}