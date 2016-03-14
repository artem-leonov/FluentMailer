using FluentMailer.Factory.Factories;
using FluentMailer.Interfaces;

namespace FluentMailer.Factory
{
    public static class FluentMailerFactory
    {
        public static IFluentMailer Create()
        {
            var mesageBodyCreatorFactory = new FluentMailerMessageBodyCreatorFactory();
            var mailer = new FluentMailer(mesageBodyCreatorFactory);

            return mailer;
        }
    }
}