using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailSender.Core;

namespace EmailSenderUnitTests.CoreTests
{
    public class ApiKeyHelperTests
    {
        [Fact]
        public void CreateConfigTest()
        {
            var apiKey = "123";
            Assert.Equal("123", apiKey.CreateConfig().ApiKey.First().Value);
        }
    }
}
