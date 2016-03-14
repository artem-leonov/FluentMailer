using FluentMailer.Interfaces;

namespace FluentMailer.Factories.Interfaces
{
    public interface IFluentMailerMessageBodyCreatorFactory
    {
        IFluentMailerMessageBodyCreator Create();
    }
}