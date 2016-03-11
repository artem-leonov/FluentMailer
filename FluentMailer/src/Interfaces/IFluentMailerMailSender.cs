using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentMailer.Interfaces
{
    public interface IFluentMailerMailSender
    {
        IFluentMailerMailSender WithReceiver(string email);
        IFluentMailerMailSender WithReceivers(IEnumerable<string> emails);
        IFluentMailerMailSender WithSubject(string subject);
        void Send();
        Task SendAsync();
    }
}