using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Model.Exceptions
{
    public class InvalidModelException : ApplicationException
    {
        public Type ModelType { get; private set; }

        public IList<string> ErrorMessages { get; private set; }

        public InvalidModelException(IList<string> errorMessages) => ErrorMessages = errorMessages;

        public InvalidModelException(Type modelType, IList<string> errorMessages)
        {
            ModelType = modelType;
            ErrorMessages = errorMessages;
        }

        public InvalidModelException(
          IList<string> errorMessages,
          string message,
          Exception innerException)
          : base(message, innerException)
        {
            ErrorMessages = errorMessages;
        }

        protected InvalidModelException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}
