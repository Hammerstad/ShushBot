using System;
using System.IO;
using System.Text.Json;

namespace ShushBot
{
    /// <summary>
    /// Represents the images supplied in images.json
    /// </summary>
    public class Image
    {
        private static int currentIndex;
        private static readonly Lazy<string[]> imageUrls = new Lazy<string[]>(LoadImageUrls);

        /// <summary>
        /// Returns a new image from images.json. Loops back after we've gone through all of them.
        /// </summary>
        public static string NextUrl
        {
            get
            {
                var index = currentIndex++;

                if (currentIndex > imageUrls.Value.Length)
                {
                    currentIndex = 0;
                }

                return imageUrls.Value[index];
            }
        }

        /// <summary>
        /// Loads all of the image urls from images.json
        /// </summary>
        /// <exception cref="ShushBotException">Thrown if images.json does not exist, or if it is ill formed.</exception>
        private static string[] LoadImageUrls()
        {
            if (!File.Exists("images.json"))
            {
                throw new ShushBotException("Did not find images.json. Either turn off showing images in the settings, or provide an images.json file.");
            }

            var images = File.ReadAllText("images.json");
            try
            {
                return JsonSerializer.Deserialize<string[]>(images);
            }
            catch
            {
                throw new ShushBotException("Unable to deserialize images.json. Please verify that it is a valid json array of strings.");
            }
        }
    }
}