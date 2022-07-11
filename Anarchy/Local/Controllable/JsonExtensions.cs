using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Discord
{
    internal static class JsonExtensions
    {
        internal static bool TryFindTypes(Type type, out Dictionary<int, Type> types)
        {
            if (DeepJsonConverter.RecognizedTypes.TryGetValue(type, out types))
                return true;
            else if (type.BaseType == null)
            {
                types = null;
                return false;
            }
            else
                return TryFindTypes(type.BaseType, out types);
        }

        public static T ParseDeterministic<T>(this /*JObject*/ JsonObject obj)
        {
            if (TryFindTypes(typeof(T), out Dictionary<int, Type> types))
            {
                int type = obj["type"].GetValue<int>();
                return (T)obj.Deserialize(types.TryGetValue(type, out var t) ? t : typeof(T));
            }
            else
                throw new InvalidCastException("Unable to find any implementations for T");
        }

        public static List<T> MultipleDeterministic<T>(this /*JArray*/ JsonArray arr)
        {
            var results = new List<T>();

            foreach (/*JObject*/ JsonObject child in arr)
                results.Add(child.ParseDeterministic<T>());

            return results;
        }
    }
}