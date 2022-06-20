using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticEmail.Model;
using EmailSender;

namespace EmailSenderUnitTests
{
    public class SampleInputProvider : IInputProvider
    {
        public bool _mergeFileProvided;
        public SampleInputProvider(bool mergeFile)
        {
            _mergeFileProvided = mergeFile;
        }
        public string GetApiKey()
        {
            return "123";
        }

        private EmailContent GetContent()
        {
            return new EmailContent
            {
                Body = new List<BodyPart>
                {
                    new BodyPart(BodyContentType.PlainText, "Hello World!")
                },
                Subject = "subject",
                From = "mail@mail.com",
            };
        }
        public EmailMessageData GetMessageData()
        {
            var recipients = new List<EmailRecipient>
            {
                new ("recipient@mail.com")
            };
            return new EmailMessageData(recipients, GetContent());
        }

        public MergeEmailPayload GetMergeEmailPayload()
        {
            return new MergeEmailPayload(new MessageAttachment(new byte[] { 1, 2, 3 }, "csv.csv"), GetContent());
        }

        public bool GetIsMergeFileProvided()
        {
            return _mergeFileProvided;
        }
    }
}
