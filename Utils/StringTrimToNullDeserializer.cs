using System.Text.Json;
using System.Text.Json.Serialization;

namespace iPortal.Utils
{
    public class StringTrimToNullConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString()?.Trim();
            return string.IsNullOrEmpty(value) ? null : value;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}