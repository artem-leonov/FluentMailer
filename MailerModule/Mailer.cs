using System.Runtime.CompilerServices;
using MailerModule.Interfaces;
using MailerModule.TemplateBases;
using MailerModule.TemplateResolvers;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace MailerModule
{
    internal class Mailer: IMailer
    {

        public Mailer()
        {
            var templateConfig = new TemplateServiceConfiguration
            {
                BaseTemplateType = typeof(MailerModuleTemplateBase<>),
                Resolver = new MailerModuleTemplateResolver()
            };
            Razor.SetTemplateService(new TemplateService(templateConfig));
        }

        public IMailerMessageBodyCreator CreateMessage()
        {
            return new MailSender();
        }
    }
}