using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticEmail.Model;

namespace EmailSender
{
    internal interface IInputProvider
    {
        public string GetApiKey();

        public EmailMessageData GetMessageData();

        public MergeEmailPayload GetMergeEmailPayload();

        public bool GetIsMergeFileProvided();
    }
}
