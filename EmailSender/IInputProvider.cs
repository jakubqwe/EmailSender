using ElasticEmail.Model;

namespace EmailSender;

internal interface IInputProvider
{
    public string GetApiKey();

    public EmailMessageData GetMessageData();

    public MergeEmailPayload GetMergeEmailPayload();

    public bool GetIsMergeFileProvided();

    public bool GetContinue();
}