using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Discord.WebSockets
{
    public class DiscordWebSocketMessage<TOpcode> : DiscordWebSocketRequest</*JToken*/JsonNode, TOpcode> where TOpcode : Enum
    {
        // these members only apply to the Gateway :P
        [JsonPropertyName("t")]
        public string EventName { get; private set; }


        [JsonPropertyName("s")]
        public uint? Sequence { get; private set; }
    }
}
