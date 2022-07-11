using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Discord
{
    internal class DiscordResponse
    {
        [JsonPropertyName("status")]
        public int Status { get; private set; }

        [JsonPropertyName("body")]
        public /*JToken*/JsonNode Body { get; private set; }
    }
}
