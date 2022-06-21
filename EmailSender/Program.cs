using ElasticEmail.Api;

namespace EmailSender;

internal class Program
{
    public static void Main(string[] args)
    {
        var engine = new Engine(new EmailsApi(), new ConsoleInputProvider());
        engine.Run();
    }
}