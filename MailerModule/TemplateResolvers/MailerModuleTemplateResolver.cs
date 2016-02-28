using System;
using System.IO;
using System.Web;
using MailerModule.Extensions;
using RazorEngine.Templating;

namespace MailerModule.TemplateResolvers
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