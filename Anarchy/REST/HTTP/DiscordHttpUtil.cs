using System.Text.Json;
using System.Text.Json.Nodes;

namespace Discord
{
    internal static class DiscordHttpUtil
    {
        public static string BuildBaseUrl(uint apiVersion, DiscordReleaseChannel releaseChannel) =>
            $"https://{(releaseChannel == DiscordReleaseChannel.Stable ? "" : releaseChannel.ToString().ToLower() + ".")}discord.com/api/v{apiVersion}";

        public static void ValidateResponse(int statusCode, /*JToken*/JsonNode body)
        {
            if (statusCode >= 400)
            {
                if (statusCode == 429)
                    throw new RateLimitException(body["retry_after"].GetValue<int>());
                else
                    throw new DiscordHttpException(body.Deserialize<DiscordHttpError>());
            }
        }
    }
}
