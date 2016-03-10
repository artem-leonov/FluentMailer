using FluentMailer.Interfaces;
using Microsoft.Practices.Unity;

namespace FluentMailer.Unity.App_Start
{
    internal class UnityConfig
    {
        public static void Configure(IUnityContainer container)
        {
            container.RegisterType<IFluentMailer, FluentMailer>();
            container.RegisterType<IFluentMailerMailSender, FluentMailerMailSender>(new TransientLifetimeManager());
        }
    }
}