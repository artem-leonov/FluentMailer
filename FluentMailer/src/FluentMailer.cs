using FluentMailer.Interfaces;
using FluentMailer.TemplateBases;
using FluentMailer.TemplateResolvers;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace FluentMailer
{
    internal class FluentMailer: IFluentMailer
    {

        public FluentMailer()
        {
            var templateConfig = new TemplateServiceConfiguration
            {
                BaseTemplateType = typeof(MailerModuleTemplateBase<>),
                Resolver = new MailerModuleTemplateResolver()
            };
            Razor.SetTemplateService(new TemplateService(templateConfig));
        }

        public IFluentMailerMessageBodyCreator CreateMessage()
        {
            return new FluentMailerMailSender();
        }
    }
}