using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ShushBot
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("settings.json", false, true)
                .AddJsonFile("settings.dev.json", true, true)
                .AddEnvironmentVariables("SHUSH_")
                .Build();

            var settings = new SlackBotSettings();
            config.Bind(settings);

            try
            {
                var bot = new SlackBot(settings);
                await bot.Run();
            }
            catch (ShushBotException botException)
            {
                Console.WriteLine(botException.Message);
                return 1;
            }

            return 0;
        }
    }
}
