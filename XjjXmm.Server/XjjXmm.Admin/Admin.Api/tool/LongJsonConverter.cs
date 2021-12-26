using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Admin.Api.tool
{
    public class LongJsonConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jt = JValue.ReadFrom(reader);

            return jt.Value<long>();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(long).Equals(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }
}
