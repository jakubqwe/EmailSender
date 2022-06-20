using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticEmail.Model;

namespace EmailSender.Core
{
    public class EmailContentBuilder
    {
        private readonly EmailContent _content;

        public EmailContentBuilder()
        {
            _content = new EmailContent();
        }

        public EmailContentBuilder AddSenderEmail(string email)
        {
            _content.From = email;
            return this;
        }

        public EmailContentBuilder AddEmailBody(string emailBody, BodyContentType bodyType)
        {
            _content.Body ??= new List<BodyPart>();
            _content.Body.Add(new BodyPart(bodyType, emailBody));
            return this;
        }

        public EmailContentBuilder AddSubject(string? subject)
        {
            _content.Subject = subject;
            return this;
        }

        public EmailContent GetContent()
        {
            return _content;
        }
        
    }
}
