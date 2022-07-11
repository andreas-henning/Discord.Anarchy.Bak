using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Discord
{
    public class DiscordHttpException : Exception
    {
        public DiscordError Code { get; private set; }
        public string ErrorMessage { get; private set; }

        public FieldErrorDictionary InvalidFields { get; private set; }

        public DiscordHttpException(DiscordHttpError error) : base($"{(int)error.Code} {error.Message}")
        {
            Code = error.Code;
            ErrorMessage = error.Message;

            if (error.Fields.GetArrayLength() > 0)
                InvalidFields = FindErrors(error.Fields);
        }

        private static FieldErrorDictionary FindErrors(/*JObject*/ JsonElement obj)
        {
            var dict = new FieldErrorDictionary();
            foreach (JsonProperty property in obj.EnumerateObject())
            {
                string key = property.Name;
                if (key == "_errors")
                    dict.Errors = property.Value.Deserialize<List<DiscordFieldError>>();
                else
                    dict[key] = FindErrors(property.Value);
            }
            return dict;
        }
    }
}