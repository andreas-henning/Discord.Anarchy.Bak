using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Discord
{
    public class MessageProperties
    {
        public MessageProperties()
        {
            _nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        }


        [JsonPropertyName("content")]
        public string Content { get; set; }


        [JsonPropertyName("nonce")]
#pragma warning disable IDE0052
        private readonly string _nonce;
#pragma warning restore


        [JsonPropertyName("tts")]
        public bool Tts { get; set; }


        [JsonPropertyName("message_reference")]
        public MessageReference ReplyTo { get; set; }


        [JsonPropertyName("embed")]
        public DiscordEmbed Embed { get; set; }


        [JsonPropertyName("components")]
        public List<MessageComponent> Components { get; set; }


        public bool ShouldSerializeReplyTo()
        {
            return ReplyTo != null;
        }

        public bool ShouldSerializeEmbed()
        {
            return Embed != null;
        }

        public bool ShouldSerializeComponents()
        {
            return Components != null;
        }
    }
}
