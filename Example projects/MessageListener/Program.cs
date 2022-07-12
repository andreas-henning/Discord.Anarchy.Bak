using System;
using System.Threading;
using Discord;
using Discord.Gateway;

namespace MessageLogger
{
    class Program
    {
        static void Main(string[] _)
        {
            // To watch the requests/responses shared between client and discord server,
            //  install fiddler and setup the default proxy as shown below:
            // HttpClient.DefaultProxy = new WebProxy("127.0.0.1", 8888);

            var client = new DiscordSocketClient();
            client.OnLoggedIn += OnLoggedIn;
            client.OnMessageReceived += OnMessageReceived;

            Console.Write("Token: ");
            client.Login(Console.ReadLine());

            Thread.Sleep(-1);
        }


        private static void OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Console.WriteLine($"Logged into {args.User}");
        }


        private static void OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
            string from;

            DiscordChannel channel = client.GetChannel(args.Message.Channel.Id);

            if (channel.InGuild)
                from = $"#{channel.Name} / {client.GetCachedGuild(((TextChannel)channel).Guild.Id).Name}";
            else
            {
                var privChannel = (PrivateChannel)channel;
                from = privChannel.Name ?? privChannel.Recipients[0].ToString();
            }

            Console.WriteLine($"[{from}] {args.Message.Author}: {args.Message.Content}");
        }
    }
}
