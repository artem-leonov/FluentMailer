using FluentMailer.Factories.Interfaces;
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
        private readonly IFluentMailerMessageBodyCreatorFactory _fluentMailerMessageBodyCreatorFactory;

        public FluentMailer(IFluentMailerMessageBodyCreatorFactory fluentMailerMessageBodyCreatorFactory)
        {
            _fluentMailerMessageBodyCreatorFactory = fluentMailerMessageBodyCreatorFactory;

            var templateConfig = new TemplateServiceConfiguration
            {
                BaseTemplateType = typeof(MailerModuleTemplateBase<>),
                Resolver = new MailerModuleTemplateResolver()
            };
            Razor.SetTemplateService(new TemplateService(templateConfig));
        }

        public IFluentMailerMessageBodyCreator CreateMessage()
        {
            return _fluentMailerMessageBodyCreatorFactory.Create();
        }
    }
}