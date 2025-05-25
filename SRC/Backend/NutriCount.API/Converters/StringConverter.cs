using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace NutriCount.API.Converters
{
    public partial class StringConverter : JsonConverter<string> //Remove espaços em branco
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString()?.Trim();

            if (value is null)
                return null;

            return RemoveExtraWhiteSpaces().Replace(value, " ");
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) => writer.WriteStringValue(value);

        [GeneratedRegex(@"\s+")]
        private static partial Regex RemoveExtraWhiteSpaces();
    }
}
