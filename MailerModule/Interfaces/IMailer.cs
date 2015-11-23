namespace MailerModule.Interfaces
{
    public interface IMailer
    {
        IMailerMessageBodyCreator CreateMessage();
    }
}