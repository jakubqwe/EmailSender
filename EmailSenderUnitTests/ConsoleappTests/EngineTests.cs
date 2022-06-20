using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticEmail.Api;
using EmailSender;

namespace EmailSenderUnitTests.ConsoleappTests
{
    public class EngineTests
    {
        [Fact]
        public void RunTest()
        {
            var engine = new Engine(new SampleEmailsApi(), new SampleInputProvider(true), false);
            engine.Run();
            engine = new Engine(new SampleEmailsApi(), new SampleInputProvider(false), false);
            engine.Run();
        }

        [Fact]
        public void SendMergeTest()
        {
            var engine = new Engine(new SampleEmailsApi(), new SampleInputProvider(true), false);
            var send = engine.SendEmailFromMergePayload();
            Assert.Equal("42", send.TransactionID);
        }

        [Fact]
        public void SendNormalTest()
        {
            var engine = new Engine(new SampleEmailsApi(), new SampleInputProvider(false), false);
            var send = engine.SendEmailFromMessageData();
            Assert.Equal("24", send.TransactionID);
        }
    }
}
