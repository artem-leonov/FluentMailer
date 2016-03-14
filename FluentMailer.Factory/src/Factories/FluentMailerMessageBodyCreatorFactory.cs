using FluentMailer.Factories.Interfaces;
using FluentMailer.Interfaces;

namespace FluentMailer.Factory.Factories
{
    internal class FluentMailerMessageBodyCreatorFactory: IFluentMailerMessageBodyCreatorFactory
    {
        public IFluentMailerMessageBodyCreator Create()
        {
            return new FluentMailerMailSender();
        }
    }
}