using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SlackConnector;
using SlackConnector.Models;

namespace ShushBot
{
    internal class Program
    {
        private static readonly string[] ImageUrls =
        {
            "https://dbstatic.no/70297307.jpg?imageId=70297307&x=0&y=1.1544011544012&cropw=100&croph=84.848484848485&width=640&height=385",
            "https://g.acdn.no/obscura/API/dynamic/r1/ece5/tr_660_440_l_f/0000/fred/2015/6/23/22/1435091735637.png__.jpg?chk=6E22C6",
            "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQvEksUctdJSDq0Og_yEUn8SNRBHrXYmaU5ms5syZcm-LizP1wMLw"
        };

        private static ISlackConnection connection;
        private static SlackChatHub channel;
        private static int currentIndex;
        private static string keyword;
        private static string phrase;
        private static bool useImage;

        private static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.dev.json", true, true)
                .Build();

            var connector = new SlackConnector.SlackConnector();
            connection = await connector.Connect(config["accessToken"]);

            var channels = await connection.GetChannels();
            channel = channels.Single(x => x.Name == config["channel"]);
            keyword = config["keyword"];
            useImage = bool.Parse(config["use_image"]);
            phrase = config["phrase"];

            connection.OnMessageReceived += MessageReceived;

            while (true) { }
        }

        private static async Task MessageReceived(SlackMessage message)
        {
            if (message.Text.ToLower() == keyword)
            {
                await Shush();
            }
        }

        private static async Task Shush()
        {
            var message = new BotMessage
            {
                Text = phrase,
                ChatHub = channel
            };

            if (useImage)
            {
                message.Attachments = new List<SlackAttachment>()
                {
                    new SlackAttachment
                    {
                        ImageUrl = GetNextImage()
                    }
                };
            }

            await connection.Say(message);
        }

        private static string GetNextImage()
        {
            var index = currentIndex++;

            if (currentIndex > ImageUrls.Length)
            {
                currentIndex = 0;
            }

            return ImageUrls[index];
        }
    }
}
