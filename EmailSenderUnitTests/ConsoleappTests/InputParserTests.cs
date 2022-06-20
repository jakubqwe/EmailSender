using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticEmail.Model;
using EmailSender;

namespace EmailSenderUnitTests.ConsoleappTests
{
    public class InputParserTests
    {
        [Fact]
        public void GetContentTypeTest()
        {
            Assert.Equal(BodyContentType.HTML, InputParser.GetContentType("a.html"));
            Assert.Equal(BodyContentType.HTML, InputParser.GetContentType("a.htm"));
            Assert.Equal(BodyContentType.PlainText, InputParser.GetContentType("a.abc"));
            Assert.Equal(BodyContentType.PlainText, InputParser.GetContentType("a.txt"));
        }
        
        [Fact]
        public void TestEmailValidation()
        {
            Assert.True(InputParser.ValidateMail("a.b@gmail.com"));
            Assert.True(InputParser.ValidateMail("a.b+1@gmail.com"));
            Assert.True(InputParser.ValidateMail("A B <a.b+1@gmail.com>"));
            Assert.False(InputParser.ValidateMail("AB <a b c@d.ddd"));
            Assert.False(InputParser.ValidateMail("A B"));
            Assert.False(InputParser.ValidateMail("A@B"));
            Assert.False(InputParser.ValidateMail("A B@email.com"));
        }

        [Fact]
        public void TestMultipleEmailsValidation()
        {
            var mails = new List<string>
            {
                "a.a@mail.com",
                "John Doe <jd@mail.com>",
            };
            Assert.Equal(-1, InputParser.ValidateMails(mails));

            mails.Add("42");
            Assert.Equal(2, InputParser.ValidateMails(mails));
        }

        [Fact]
        public void TestParseContent()
        {
            var content = InputParser.ParseContent("ABC@mail.com", "<h1>Hello</h1>", BodyContentType.HTML, "Hello");
            Assert.Equal("ABC@mail.com", content.From);
            Assert.Equal("Hello", content.Subject);
            Assert.Equal(1, content.Body.Count);
            Assert.Equal(BodyContentType.HTML, content.Body[0].ContentType);
        }

        [Fact]
        public void TestParseMergePayload()
        {
            var content = InputParser.ParseContent("ABC@mail.com", "<h1>Hello</h1>", BodyContentType.HTML, "Hello");
            var payload = InputParser.ParseMergePayload(content, new byte[] { 1, 2, 3 }, "csv.csv");

            Assert.Equal(new byte[] { 1, 2, 3 }, payload.MergeFile.BinaryContent);
            Assert.Equal("csv.csv", payload.MergeFile.Name);
            Assert.Equal(content, payload.Content);

        }

        [Fact]

        public void TestParseMessageData()
        {
            var content = InputParser.ParseContent("ABC@mail.com", "<h1>Hello</h1>", BodyContentType.HTML, "Hello");
            var messageData = InputParser.ParseMessageData(content, "a.b@mail.com; b.c@mail.com; John Doe <jd@mail.com>");
            Assert.Equal(3, messageData.Recipients.Count);
            Assert.Equal("b.c@mail.com", messageData.Recipients[1].Email);
            Assert.Equal("John Doe <jd@mail.com>", messageData.Recipients[2].Email);
            Assert.Equal(content, messageData.Content);
        }

        [Fact]
        public void TestExceptions()
        {
            Assert.Throws<ArgumentException>(() => InputParser.ParseContent("AB", "<h1>Hello</h1>", BodyContentType.HTML, "Hello"));

            var content = InputParser.ParseContent("ABC@mail.com", "<h1>Hello</h1>", BodyContentType.HTML, "Hello");
            Assert.Throws<ArgumentException>(() => InputParser.ParseMessageData(content, "A@mail.com; AAAAAAA"));
        }
    }
}
