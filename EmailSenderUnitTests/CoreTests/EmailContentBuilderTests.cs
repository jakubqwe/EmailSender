using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticEmail.Model;
using EmailSender.Core;

namespace EmailSenderUnitTests.CoreTests
{
    public class EmailContentBuilderTests
    {
        [Fact]
        public void BuildContentTest()
        {
            var builder = new EmailContentBuilder();
            builder.AddSenderEmail("abc@mail.com")
                .AddEmailBody("<h1>Hello World</h1>", BodyContentType.HTML)
                .AddSubject(null);
            var content = builder.GetContent();
            Assert.Equal("abc@mail.com", content.From);
            Assert.Equal("<h1>Hello World</h1>", content.Body[0].Content);
            Assert.Equal(BodyContentType.HTML, content.Body[0].ContentType);
            Assert.Null(content.Subject);

            content = builder.AddSubject("Test").GetContent();
            Assert.Equal("Test", content.Subject);
        }
    }
}
