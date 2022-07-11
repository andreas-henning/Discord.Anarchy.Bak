using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Discord.Gateway
{
    public class DiscordGameActivity : DiscordActivity
    {
        [JsonPropertyName("application_id")]
        public string ApplicationId { get; private set; }


        [JsonPropertyName("timestamps")]
        private readonly /*JObject*/ JsonObject _obj;

        public DateTimeOffset? Since
        {
            get
            {
                if (_obj != null)
                    return DateTimeOffset.FromUnixTimeMilliseconds(_obj["start"].GetValue<long>());
                else
                    return null;
            }
        }
    }
}
