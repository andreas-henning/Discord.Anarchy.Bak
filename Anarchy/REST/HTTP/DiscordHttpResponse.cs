using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Discord
{
    public class DiscordHttpResponse
    {
        public int StatusCode { get; private set; }
        public /*JToken*/JsonNode Body { get; private set; }

        public DiscordHttpResponse(int statusCode, string content)
        {
            StatusCode = statusCode;
            if (content != null && content.Length != 0)
                Body = /*JToken*/JsonNode.Parse(content);
        }


        public T Deserialize<T>()
        {
            return Body.ToObject<T>();
        }


        public T ParseDeterministic<T>()
        {
            return ((/*JObject*/ JsonObject)Body).ParseDeterministic<T>();
        }

        public List<T> MultipleDeterministic<T>()
        {
            return ((/*JArray*/ JsonArray)Body).MultipleDeterministic<T>();
        }
    }
}
