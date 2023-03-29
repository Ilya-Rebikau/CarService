using System.Runtime.Serialization;

namespace CarService.UserAPI.Infrastructure
{
    [Serializable]
    public class MyValidationException : Exception
    {
        public MyValidationException()
        {
        }
        public MyValidationException(string message)
            : base(message)
        {
        }
        public MyValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        protected MyValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
