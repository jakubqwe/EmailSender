using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticEmail.Client;

namespace EmailSender.Core
{
    public static class ApiKeyHelper
    {
        public static Configuration CreateConfig(this string apiKey)
        {
            var config = new Configuration();
            config.ApiKey.Add(ElasticEmailApiConstants.ApiKeyParameterName, apiKey);
            return config;
        }
    }
}
