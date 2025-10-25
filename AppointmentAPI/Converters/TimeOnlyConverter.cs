using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppointmentAPI.Converters
{
    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        private string format = "HH:mm";
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (TimeOnly.TryParseExact(value, format, out TimeOnly res))
            {
                return res;
            }
               

            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(format));
        }
    }
}
