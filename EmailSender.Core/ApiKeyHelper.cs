using ElasticEmail.Client;

namespace EmailSender.Core;

public static class ApiKeyHelper
{
    public static Configuration CreateConfig(this string apiKey)
    {
        var config = new Configuration();
        config.ApiKey.Add(ElasticEmailApiConstants.ApiKeyParameterName, apiKey);
        return config;
    }
}