using System.Net;

namespace NutriCount.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : NutriCountException
    {
        public IList<string> ErrorMessages { get; set; }
        public ErrorOnValidationException(IList<string> errorMessages)
    : base(string.Empty)
        {
            ErrorMessages = errorMessages;
        }

        public override IList<string> GetErrorMessages()
        {
            return ErrorMessages;
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest; // Código HTTP ideal para erro de validação
        }
    }
}
