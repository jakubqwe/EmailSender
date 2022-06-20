using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticEmail.Model;
using EmailSender.Core;

namespace EmailSender
{
    internal class ConsoleInputProvider : IInputProvider
    {
        public string GetApiKey()
        {
            Console.WriteLine("API key: ");

            var apiKeyBuilder = new StringBuilder();
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    apiKeyBuilder.Append(keyInfo.KeyChar);
                }

            } while (key != ConsoleKey.Enter);

            return apiKeyBuilder.ToString();
        }

        private static string? GetString(string message, bool required = true)
        {
            Console.WriteLine(message);
            var response = Console.ReadLine();
            if (required && string.IsNullOrEmpty(response))
            {
                throw new ArgumentException("This data is required");
            }
            
            return response;
        }

        private EmailContent GetContent()
        {
            var sender = GetString("Sender email: ");
            var bodyPath = GetString("Email file path: ").Trim('\"');
            var contentType = InputParser.GetContentType(bodyPath);
            var subject = GetString("Subject (optional): ", false);
            return InputParser.ParseContent(sender, File.ReadAllText(bodyPath), contentType, subject);
        }

        public bool GetIsMergeFileProvided()
        {
            Console.WriteLine("Do you want to input a CSV merge file?[y/N]");
            string input;
            do
            {
                input = Console.ReadLine().ToLowerInvariant();
            } while (input != "y" && input != "n");

            return input == "y";
        }
        public EmailMessageData GetMessageData()
        {
            var recipients = GetString("Recipient emails (delimited by ;): ");
            var content = GetContent();
            return InputParser.ParseMessageData(content, recipients);
        }
        public MergeEmailPayload GetMergeEmailPayload()
        {
            var mergeFilePath = GetString("Merge file path: ").Trim('\"');
            var content = GetContent();
            return InputParser.ParseMergePayload(content, File.ReadAllBytes(mergeFilePath), Path.GetFileName(mergeFilePath));
        }
        
    }
    
}
