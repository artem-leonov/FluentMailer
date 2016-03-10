namespace FluentMailer.Interfaces
{
    public interface IFluentMailer
    {
        IFluentMailerMessageBodyCreator CreateMessage();
    }
}