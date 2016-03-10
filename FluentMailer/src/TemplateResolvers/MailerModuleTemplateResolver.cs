using System.IO;
using FluentMailer.Extensions;
using RazorEngine.Templating;

namespace FluentMailer.TemplateResolvers
{
    public class MailerModuleTemplateResolver: ITemplateResolver
    {
        public string Resolve(string name)
        {
            var path = name.ResolvePath();

            return File.ReadAllText(path, System.Text.Encoding.Default);
        }
    }
}