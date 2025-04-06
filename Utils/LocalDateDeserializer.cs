using System.Text.Json;
using System.Text.Json.Serialization;

namespace iPortal.Utils
{
    public class LocalDateConverter : JsonConverter<DateTime?>
    {
        private static readonly string DateFormat = "dd-MM-yyyy";

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString()?.Trim();
            if (string.IsNullOrEmpty(dateString))
            {
                return null; // Nếu chuỗi rỗng, trả về null
            }
            return DateTime.ParseExact(dateString, DateFormat, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString(DateFormat));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}