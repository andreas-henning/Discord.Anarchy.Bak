using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

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
