using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentMailer.Extensions;
using FluentMailer.Interfaces;
using RazorEngine;
using Encoding = System.Text.Encoding;

namespace FluentMailer
{
    internal class FluentMailerMailSender : IFluentMailerMessageBodyCreator, IFluentMailerMailSender
    {
        private string _viewBody;
        private string _subject;
        private readonly ICollection<string> _receivers;
        private readonly ICollection<Attachment> _attachments; 

        public FluentMailerMailSender()
        {
            _receivers = new List<string>();
            _attachments = new List<Attachment>();
        }

        public IFluentMailerMailSender WithView<T>(string viewPath, T model)
        {
            var path = viewPath.ResolvePath();
            if (!File.Exists(path))
            {
                throw new ArgumentException(string.Format("View with path {0} not found", path));
            }

            var viewContent = File.ReadAllText(path);
            _viewBody = Razor.Parse(viewContent, model);

            return this;
        }

        public IFluentMailerMailSender WithView(string viewPath)
        {
            var path = viewPath.ResolvePath();

            if (!File.Exists(path))
            {
                throw new ArgumentException(string.Format("View with path {0} not found", path));
            }

            var viewContent = File.ReadAllText(path);
            _viewBody = Razor.Parse(viewContent, new object());

            return this;
        }

        public IFluentMailerMailSender WithViewBody(string viewBody)
        {
            if (viewBody == null)
            {
                throw new ArgumentNullException("View body cannot be a null");
            }

            _viewBody = viewBody;

            return this;
        }

        public IFluentMailerMailSender WithReceiver(string email)
        {
            if (!Regex.IsMatch(email, @"^([a-zA-Zа-яА-Я0-9_\-\.]+)@([a-zA-Zа-яА-Я0-9\-\.]+)\.([a-zA-Zа-яА-Я0-9\-]+)$",
                    RegexOptions.IgnoreCase))
            {
                throw new ArgumentException(string.Format("Email {0} is incorrect", email));
            }

            email = email.ToLower();

            if (!_receivers.Contains(email))
            {
                _receivers.Add(email);
            }

            return this;
        }

        public IFluentMailerMailSender WithReceivers(IEnumerable<string> emails)
        {
            if (emails.Any(
                    x =>
                        !Regex.IsMatch(x,
                            @"^([a-zA-Zа-яА-Я0-9_\-\.]+)@([a-zA-Zа-яА-Я0-9\-\.]+)\.([a-zA-Zа-яА-Я0-9\-]+)$",
                            RegexOptions.IgnoreCase)))
            {
                throw new ArgumentException("Some emails are incorrect");
            }

            var uniqEmails = emails
                .Select(x => x.ToLower())
                .Where(x => !_receivers.Contains(x));

            foreach (var uniqEmail in uniqEmails)
            {
                WithReceiver(uniqEmail);
            }

            return this;
        }

        public IFluentMailerMailSender WithSubject(string subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException("Subject cannot be a null");
            }

            _subject = subject;

            return this;
        }

        public IFluentMailerMailSender WithAttachment(string filename)
        {
            _attachments.Add(new Attachment(filename.ResolvePath()));

            return this;
        }

        public IFluentMailerMailSender WithAttachment(Stream fileContent, string filename)
        {
            _attachments.Add(new Attachment(fileContent, filename));

            return this;
        }

        public void Send()
        {
            var message = new MailMessage();
            message.BodyEncoding = Encoding.UTF8;
            message.SubjectEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Body = _viewBody;

            foreach (var receiver in _receivers)
            {
                message.To.Add(receiver);
            }

            foreach (var attachment in _attachments)
            {
                message.Attachments.Add(attachment);
            }

            if (_subject != null)
            {
                message.Subject = _subject;
            }

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Send(message);
            }
        }

        public async Task SendAsync()
        {
            var message = new MailMessage();
            message.BodyEncoding = Encoding.UTF8;
            message.SubjectEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Body = _viewBody;

            foreach (var receiver in _receivers)
            {
                message.To.Add(receiver);
            }

            if (_subject != null)
            {
                message.Subject = _subject;
            }

            var smtpClient = new SmtpClient();

            await Task.Run(() =>
            {
                smtpClient.Send(message);
                smtpClient.Dispose();
            });
        }
    }
}