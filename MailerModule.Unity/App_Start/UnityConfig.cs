using MailerModule.Interfaces;
using Microsoft.Practices.Unity;

namespace MailerModule.Unity
{
    internal class UnityConfig
    {
        public static void Configure(IUnityContainer container)
        {
            container.RegisterType<IMailer, Mailer>();
            container.RegisterType<IMailSender, MailSender>(new TransientLifetimeManager());
        }
    }
}