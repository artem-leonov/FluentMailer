using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FluentMailer.Interfaces
{
    public interface IFluentMailerMailSender
    {
        IFluentMailerMailSender WithReceiver(string email);
        IFluentMailerMailSender WithReceivers(params string[] emails);
        IFluentMailerMailSender WithSubject(string subject);
        IFluentMailerMailSender WithAttachment(string filename);
        IFluentMailerMailSender WithAttachment(Stream fileContent, string filename);
        void Send();
        Task SendAsync();
    }
}