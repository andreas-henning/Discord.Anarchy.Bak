using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Discord
{
    public class Language : JsonConverter<DiscordLanguage>
    {
        private static readonly Dictionary<DiscordLanguage, string> Languages = new()
        {
            { DiscordLanguage.Danish, "da" },
            { DiscordLanguage.German, "de" },
            { DiscordLanguage.EnglishUK, "en-GB" },
            { DiscordLanguage.EnglishUS, "en-US" },
            { DiscordLanguage.Spanish, "es-ES" },
            { DiscordLanguage.French, "fr" },
            { DiscordLanguage.Croatian, "hr" },
            { DiscordLanguage.Italian, "it" },
            { DiscordLanguage.Hungarian, "hu" },
            { DiscordLanguage.Dutch, "nl" },
            { DiscordLanguage.Norwegian, "no" },
            { DiscordLanguage.Polish, "pl" },
            { DiscordLanguage.Portuguese, "pt-BR" },
            { DiscordLanguage.Romanian, "ro" },
            { DiscordLanguage.Finnish, "fi" },
            { DiscordLanguage.Swedish, "sv-SE" },
            { DiscordLanguage.Viatnamese, "vi" },
            { DiscordLanguage.Turkish, "tr" },
            { DiscordLanguage.Czech, "cs" },
            { DiscordLanguage.Greek, "el" },
            { DiscordLanguage.Russian, "ru" },
            { DiscordLanguage.Ukranian, "uk" },
            { DiscordLanguage.Thai, "th" },
            { DiscordLanguage.Chinese, "zh-CN" },
            { DiscordLanguage.Japanese, "ja" },
            { DiscordLanguage.Korean, "ko" },
            { DiscordLanguage.Bulgarian, "bg" },
            { DiscordLanguage.Taiwanese, "zh-TW" },
            { DiscordLanguage.Lithuanian, "lt" },
            { DiscordLanguage.Hindi, "hi" }
        };

        private static string ToString(DiscordLanguage lang)
        {
            return Languages[lang];
        }

        private static DiscordLanguage FromString(string langStr)
        {
            foreach (var language in Languages)
            {
                if (language.Value == langStr)
                    return language.Key;
            }

            throw new InvalidOperationException("Invalid language string");
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override DiscordLanguage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return FromString(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DiscordLanguage value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(ToString((DiscordLanguage)value));
        }
    }

    [JsonConverter(typeof(Language))]
    public enum DiscordLanguage
    {
        Danish,
        German,
        EnglishUK,
        EnglishUS,
        Spanish,
        French,
        Croatian,
        Italian,
        Lithuanian,
        Hungarian,
        Dutch,
        Norwegian,
        Polish,
        Portuguese,
        Romanian,
        Finnish,
        Swedish,
        Viatnamese,
        Turkish,
        Czech,
        Greek,
        Bulgarian,
        Russian,
        Ukranian,
        Thai,
        Chinese,
        Japanese,
        Taiwanese,
        Korean,
        Hindi
    }
}
