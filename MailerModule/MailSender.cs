using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using MailerModule.Extensions;
using MailerModule.Interfaces;
using RazorEngine;
using Encoding = System.Text.Encoding;

namespace MailerModule
{
    internal class MailSender: IMailerMessageBodyCreator, IMailSender
    {
        private readonly ICollection<string> _receivers; 
        private string _viewBody;
        private string _subject;

        public MailSender()
        {
            _receivers = new List<string>();
        }

        public IMailSender WithView<T>(string viewPath, T model)
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

        public IMailSender WithView(string viewPath)
        {
            var path = viewPath.ResolvePath();

            if (!File.Exists(path))
            {
                throw new ArgumentException(string.Format("View with path {0} not found", path));
            }

            var viewContent = File.ReadAllText(path);
            _viewBody = Razor.Parse(viewContent);

            return this;
        }

        public IMailSender WithViewBody(string viewBody)
        {
            if (viewBody == null)
            {
                throw new ArgumentNullException("View body cannot be a null");
            }

            _viewBody = viewBody;

            return this;
        }

        public IMailSender WithReceiver(string email)
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

        public IMailSender WithReceivers(IEnumerable<string> emails)
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

        public IMailSender WithSubject(string subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException("Subject cannot be a null");
            }

            _subject = subject;

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

            if (_subject != null)
            {
                message.Subject = _subject;
            }

            var smtpClient = new SmtpClient();
            smtpClient.Send(message);            
        }

        public void SendAsync()
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
            smtpClient.SendAsync(message, null);
        }
    }
}