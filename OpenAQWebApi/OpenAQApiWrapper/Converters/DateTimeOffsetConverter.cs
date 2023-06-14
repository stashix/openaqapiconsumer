using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Converters
{
    internal class DateTimeOffsetConverter : JsonConverter<DateTime>
    {
        private const string _format = "yyyy-MM-dd HH:mm:ss.FFFzz";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTimeOffset.ParseExact(reader.GetString() ?? string.Empty, _format, 
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal)
                .UtcDateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(new DateTimeOffset(value).ToString(_format));
        }
    }
}
