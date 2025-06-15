namespace NutriCount.Communication.Responses
{
    public class ResponseRegisteredUserJson
    {
        public string Name { get; set; } = string.Empty; //The initial value of the property will be an empty string
        public ResponseTokensJson Tokens { get; set; } = default!;
    }
}
