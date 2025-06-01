using System.Net;

namespace NutriCount.Exceptions.ExceptionsBase
{
    public abstract class NutriCountException : SystemException
    {
        public NutriCountException(string message):base(message) { }

        public abstract IList<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
