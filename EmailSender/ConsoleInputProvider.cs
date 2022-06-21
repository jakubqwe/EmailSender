using System.Text;
using ElasticEmail.Model;

namespace EmailSender;

internal class ConsoleInputProvider : IInputProvider
{
    public string GetApiKey()
    {
        Console.WriteLine("API key: ");

        var apiKeyBuilder = new StringBuilder();
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;
            if (!char.IsControl(keyInfo.KeyChar))
            {
                apiKeyBuilder.Append(keyInfo.KeyChar);
            }
        } while (key != ConsoleKey.Enter);

        return apiKeyBuilder.ToString();
    }

    public bool GetIsMergeFileProvided()
    {
        return GetYesNo("Do you want to input a CSV merge file?");
    }

    public bool GetContinue()
    {
        return GetYesNo("Send another mail?");
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
        return InputParser.ParseMergePayload(content, File.ReadAllBytes(mergeFilePath),
            Path.GetFileName(mergeFilePath));
    }

    private static string? GetString(string message, bool required = true)
    {
        string response;
        do
        {
            Console.WriteLine(message);
            response = Console.ReadLine();
        } while (required && string.IsNullOrEmpty(response));

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


    private static bool GetYesNo(string message)
    {
        string input;
        do
        {
            Console.WriteLine(message + "[y/n]");
            input = Console.ReadLine().ToLowerInvariant();
        } while (input != "y" && input != "n");

        return input == "y";
    }
}