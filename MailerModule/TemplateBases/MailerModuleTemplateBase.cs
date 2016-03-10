using System;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;

namespace MailerModule.TemplateBases
{
    public abstract class MailerModuleTemplateBase<T>: TemplateBase<T>
    {
        public string RenderPart(string templateName, object model = null)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, templateName);
            return Razor.Parse(File.ReadAllText(path), model);
        }
    }
}