using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticEmail.Api;
using ElasticEmail.Client;
using ElasticEmail.Model;
using EmailSender.Core;

namespace EmailSender
{
    internal class Engine
    {
        private readonly IEmailsApi _emailsApi;
        private readonly IInputProvider _inputProvider;
        internal bool IsMultipleInputExpected;

        public Engine(IEmailsApi emailsApi, IInputProvider inputProvider, bool isMultipleInputExpected)
        {
            _emailsApi = emailsApi;
            _inputProvider = inputProvider;
            IsMultipleInputExpected = isMultipleInputExpected;

            emailsApi.Configuration = _inputProvider.GetApiKey().CreateConfig();
        }

        public void Run()
        {
            do
            {
                try
                {
                    if (_inputProvider.GetIsMergeFileProvided())
                    {
                        SendEmailFromMergePayload();
                        continue;
                    }
                    SendEmailFromMessageData();
                }

                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

            } while (IsMultipleInputExpected);
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
}
