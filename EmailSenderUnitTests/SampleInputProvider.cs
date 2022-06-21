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
        public bool MergeFileProvided;
        public bool Rerun;
        private int count = 0;
        public SampleInputProvider(bool mergeFile, bool rerun)
        {
            MergeFileProvided = mergeFile;
            Rerun = rerun;
        }
        public string GetApiKey()
        {
            return "123";
        }

        private EmailContent GetContent()
        {
            if (count++ == 2)
            {
                Rerun = false;
            }
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
            return MergeFileProvided;
        }

        public bool GetContinue()
        {
            return Rerun;
        }
    }
}
