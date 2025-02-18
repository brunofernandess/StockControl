using System.Text.Json;
using System.Text.Json.Serialization;

namespace StockControl.Utils
{
    public class ConverterDateTime : JsonConverter<DateTime>
    {
        // método para deserializar o objeto
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString()!);
        }
        // método para serializar o objeto
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("dd/MM/yyyy"));
        }

    }
}
