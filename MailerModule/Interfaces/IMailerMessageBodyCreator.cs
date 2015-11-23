namespace MailerModule.Interfaces
{
    public interface IMailerMessageBodyCreator
    {
        IMailSender WithView<T>(string viewPath, T model);
        IMailSender WithView(string viewPath);
        IMailSender WithViewBody(string viewBody);
    }
}