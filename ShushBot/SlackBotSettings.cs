namespace ShushBot
{
    public class SlackBotSettings
    {
        /// <summary>
        /// The access token used to connect to slack. It's the one called Bot User OAuth
        /// Access Token on the webpage, and should start with "xoxb-".
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The keyword the bot will respond to.
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// The phrase the bot will respond with.
        /// </summary>
        public string Phrase { get; set; }

        /// <summary>
        /// The channel the bot will repond in.
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// Whether or not the bot will reply with an image from images.json
        /// </summary>
        public bool UseImages { get; set; }
    }
}