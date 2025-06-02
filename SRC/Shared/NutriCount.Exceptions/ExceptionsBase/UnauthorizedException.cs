using System.Net;

namespace NutriCount.Exceptions.ExceptionsBase
{
    public class UnauthorizedException : NutriCountException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
