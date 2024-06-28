using System.Text.Json.Serialization;
using System.Text.Json;

namespace Azure2Pulumi.Importer.Helpers
{
    public class TrimmingConverter : JsonConverter<string>
    {
        public override string Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => reader.GetString()?.Trim();

        public override void Write(
            Utf8JsonWriter writer,
            string value,
            JsonSerializerOptions options) => writer.WriteStringValue(value?.Trim());
    }
}
