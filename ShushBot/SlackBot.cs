using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SlackConnector;
using SlackConnector.Exceptions;
using SlackConnector.Models;

namespace ShushBot
{
    /// <summary>
    /// Internal representation of the connection to a slack workspace
    /// </summary>
    public class SlackBot
    {
        private readonly SlackBotSettings settings;

        private SlackChatHub channel;
        private ISlackConnection connection;

        /// <summary>
        /// Creates a new slack bot based on supplied settings. Does not connect to slack yet.
        /// </summary>
        public SlackBot(SlackBotSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Connects to slack and starts listening for the phrase specified in the settings.
        /// </summary>
        public async Task Run()
        {
            var connector = new SlackConnector.SlackConnector();
            try
            {
                connection = await connector.Connect(settings.AccessToken);
            }
            catch (CommunicationException)
            {
                throw new ShushBotException("Unable to connect to slack workspace using the provided access token. Ensure that it is correct.");
            }

            Console.WriteLine("Connected to slack.");

            var channels = await connection.GetChannels();
            var matchingChannel = channels.FirstOrDefault(x => x.Name == settings.Channel);

            channel = matchingChannel ?? throw new ShushBotException($"Unable to find a channel with the name {settings.Channel}");

            connection.OnMessageReceived += MessageReceived;

            Console.WriteLine($"Connected to channel {settings.Channel}");
            Console.WriteLine($"Listening to messages. If a user PMs me '{settings.Keyword}', I will respond with:\n{settings.Phrase}");
            if (settings.UseImages)
            {
                Console.WriteLine("I will also display an image provided in images.json.");
            }
            while (true) { }
        }

        private async Task MessageReceived(SlackMessage message)
        {
            if (string.Equals(message.Text, settings.Keyword, StringComparison.CurrentCultureIgnoreCase))
            {
                await Shush();
            }
        }

        private async Task Shush()
        {
            var message = new BotMessage
            {
                Text = settings.Phrase,
                ChatHub = channel
            };

            if (settings.UseImages)
            {
                message.Attachments = new List<SlackAttachment>
                {
                    new SlackAttachment
                    {
                        ImageUrl = Image.NextUrl
                    }
                };
            }

            await connection.Say(message);
        }
    }
}