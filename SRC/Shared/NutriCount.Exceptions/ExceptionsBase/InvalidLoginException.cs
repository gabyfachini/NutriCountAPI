using System.Net;

namespace NutriCount.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : NutriCountException
    {
        public InvalidLoginException() : base(ResourceMessageException.EMAIL_OR_PASSWORD_INVALID)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
