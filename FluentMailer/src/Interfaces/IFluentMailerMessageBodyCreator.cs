namespace FluentMailer.Interfaces
{
    public interface IFluentMailerMessageBodyCreator
    {
        IFluentMailerMailSender WithView<T>(string viewPath, T model);
        IFluentMailerMailSender WithView(string viewPath);
        IFluentMailerMailSender WithViewBody(string viewBody);
    }
}