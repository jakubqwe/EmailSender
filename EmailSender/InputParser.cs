using System.Text.RegularExpressions;
using ElasticEmail.Model;
using EmailSender.Core;

namespace EmailSender;

internal static class InputParser
{
    public static BodyContentType GetContentType(string filePath)
    {
        return Path.GetExtension(filePath) is ".html" or ".htm" ? BodyContentType.HTML : BodyContentType.PlainText;
    }

    internal static bool ValidateMail(string mailAddress)
    {
        var leftIndex = mailAddress.IndexOf('<');
        if (leftIndex != -1)
        {
            if (mailAddress[^1] != '>')
            {
                return false;
            }

            mailAddress = mailAddress.Substring(leftIndex + 1, mailAddress.Length - leftIndex - 2);
        }

        var regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                              + "@"
                              + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
        return regex.IsMatch(mailAddress);
    }

    internal static int ValidateMails(IList<string> recipientsList)
    {
        for (var i = 0; i < recipientsList.Count; i++)
        {
            if (!ValidateMail(recipientsList[i]))
            {
                return i;
            }
        }

        return -1;
    }

    public static EmailMessageData ParseMessageData(EmailContent content, string recipients)
    {
        var recipientsList = recipients.Split(';').Select(e => e.Trim()).ToArray();
        var invalidMailIndex = ValidateMails(recipientsList.ToArray());
        if (invalidMailIndex != -1)
        {
            throw new ArgumentException($"Invalid email address: {recipientsList[invalidMailIndex]}");
        }

        return new EmailMessageData(recipientsList.Select(e => new EmailRecipient(e)).ToList(), content);
    }

    public static MergeEmailPayload ParseMergePayload(EmailContent content, byte[] mergeFileContent, string fileName)
    {
        var mergeFile = new MessageAttachment(mergeFileContent, fileName);
        return new MergeEmailPayload(mergeFile, content);
    }

    public static EmailContent ParseContent(string sender, string body, BodyContentType contentType, string? subject)
    {
        sender = sender.Trim();
        if (!ValidateMail(sender))
        {
            throw new ArgumentException($"Invalid email address: {sender}");
        }

        var content = new EmailContentBuilder()
            .AddSenderEmail(sender)
            .AddEmailBody(body, contentType)
            .AddSubject(subject);

        return content.GetContent();
    }
}