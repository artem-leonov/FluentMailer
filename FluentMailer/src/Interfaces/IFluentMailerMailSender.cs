using System.Collections.Generic;

namespace FluentMailer.Interfaces
{
    public interface IFluentMailerMailSender
    {
        IFluentMailerMailSender WithReceiver(string email);
        IFluentMailerMailSender WithReceivers(IEnumerable<string> emails);
        IFluentMailerMailSender WithSubject(string subject);
        void Send();
        void SendAsync();
    }
}