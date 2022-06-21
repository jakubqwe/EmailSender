using ElasticEmail.Api;
using ElasticEmail.Model;
using EmailSender.Core;

namespace EmailSender;

internal class Engine
{
    private readonly IEmailsApi _emailsApi;
    private readonly IInputProvider _inputProvider;

    public Engine(IEmailsApi emailsApi, IInputProvider inputProvider)
    {
        _emailsApi = emailsApi;
        _inputProvider = inputProvider;

        emailsApi.Configuration = _inputProvider.GetApiKey().CreateConfig();
    }

    public void Run()
    {
        bool shouldRepeat;
        do
        {
            try
            {
                if (_inputProvider.GetIsMergeFileProvided())
                {
                    SendEmailFromMergePayload();
                }
                else
                {
                    SendEmailFromMessageData();
                }

                Console.WriteLine("Email sent");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            shouldRepeat = _inputProvider.GetContinue();
        } while (shouldRepeat);
    }

    public EmailSend SendEmailFromMessageData()
    {
        return _emailsApi.EmailsPost(_inputProvider.GetMessageData());
    }

    public EmailSend SendEmailFromMergePayload()
    {
        return _emailsApi.EmailsMergefilePost(_inputProvider.GetMergeEmailPayload());
    }
}