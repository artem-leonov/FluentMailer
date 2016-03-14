using FluentMailer.Factories.Interfaces;
using FluentMailer.Interfaces;
using FluentMailer.Unity.Factories;
using Microsoft.Practices.Unity;

namespace FluentMailer.Unity.App_Start
{
    internal class UnityConfig
    {
        public static void Configure(IUnityContainer container)
        {
            container.RegisterType<IFluentMailer, FluentMailer>();
            container.RegisterType<IFluentMailerMessageBodyCreator, FluentMailerMailSender>(new TransientLifetimeManager());
            
            var messageBodyCreatorFactory = new UnityFluentMailerMessageBodyCreatorFactory(container);
            container.RegisterInstance(typeof(IFluentMailerMessageBodyCreatorFactory), messageBodyCreatorFactory, new ContainerControlledLifetimeManager());
        }
    }
}