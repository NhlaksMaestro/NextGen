using System;
using System.Runtime.Serialization;

namespace NextGen.Model.Exceptions
{
    [Serializable]
    public class DataConflictException : ApplicationException
    {
        public DataConflictException()
        {
        }

        public DataConflictException(string message)
          : base(message)
        {
        }

        public DataConflictException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        protected DataConflictException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}
