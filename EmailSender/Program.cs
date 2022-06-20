using System.Diagnostics;
using System.Net.Mail;
using ElasticEmail;
using ElasticEmail.Client;
using ElasticEmail.Api;
using ElasticEmail.Model;
using EmailSender.Core;


namespace EmailSender
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var engine = new Engine(new EmailsApi(), new ConsoleInputProvider(), args.FirstOrDefault() == "-m");
            engine.Run();
        }
    }
}