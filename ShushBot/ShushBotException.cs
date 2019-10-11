using System;

namespace ShushBot
{
    /// <summary>
    /// Thrown in case a known exception occurs. The message will be printed to the user, before the app terminates.
    /// </summary>
    public class ShushBotException : Exception
    {
        /// <inheritdoc />
        public ShushBotException(string message) : base(message) { }
    }
}