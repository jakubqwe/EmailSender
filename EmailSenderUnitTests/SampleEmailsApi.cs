using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticEmail.Api;
using ElasticEmail.Client;
using ElasticEmail.Model;

namespace EmailSenderUnitTests
{
    internal class SampleEmailsApi : IEmailsApi
    {
        public IReadableConfiguration? Configuration { get; set; }
        public ExceptionFactory? ExceptionFactory { get; set; }

        public EmailData EmailsByMsgidViewGet(string msgid, int operationIndex = 0)
        {
            throw new NotImplementedException();
        }

        public Task<EmailData> EmailsByMsgidViewGetAsync(string msgid, int operationIndex = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<EmailData> EmailsByMsgidViewGetWithHttpInfo(string msgid, int operationIndex = 0)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<EmailData>> EmailsByMsgidViewGetWithHttpInfoAsync(string msgid, int operationIndex = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public EmailSend EmailsMergefilePost(MergeEmailPayload mergeEmailPayload, int operationIndex = 0)
        {
           
            var content = mergeEmailPayload.Content;
            if (content.Body.Count == 0 || content.From == null )
            {
                throw new ApiException();
            }

            return new EmailSend("42", "12345");
        }

        public Task<EmailSend> EmailsMergefilePostAsync(MergeEmailPayload mergeEmailPayload, int operationIndex = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<EmailSend> EmailsMergefilePostWithHttpInfo(MergeEmailPayload mergeEmailPayload, int operationIndex = 0)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<EmailSend>> EmailsMergefilePostWithHttpInfoAsync(MergeEmailPayload mergeEmailPayload, int operationIndex = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public EmailSend EmailsPost(EmailMessageData emailMessageData, int operationIndex = 0)
        {
            var content = emailMessageData.Content;
            if (content.Body.Count == 0 || content.From == null || emailMessageData.Recipients == null)
            {
                throw new ApiException();
            }

            return new EmailSend("24", "12345");
        }

        public Task<EmailSend> EmailsPostAsync(EmailMessageData emailMessageData, int operationIndex = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<EmailSend> EmailsPostWithHttpInfo(EmailMessageData emailMessageData, int operationIndex = 0)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<EmailSend>> EmailsPostWithHttpInfoAsync(EmailMessageData emailMessageData, int operationIndex = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public EmailSend EmailsTransactionalPost(EmailTransactionalMessageData emailTransactionalMessageData, int operationIndex = 0)
        {
            throw new NotImplementedException();
        }

        public Task<EmailSend> EmailsTransactionalPostAsync(EmailTransactionalMessageData emailTransactionalMessageData, int operationIndex = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<EmailSend> EmailsTransactionalPostWithHttpInfo(EmailTransactionalMessageData emailTransactionalMessageData, int operationIndex = 0)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<EmailSend>> EmailsTransactionalPostWithHttpInfoAsync(EmailTransactionalMessageData emailTransactionalMessageData, int operationIndex = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public string GetBasePath()
        {
            throw new NotImplementedException();
        }
    }
}
