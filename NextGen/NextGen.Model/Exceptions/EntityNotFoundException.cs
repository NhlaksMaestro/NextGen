using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Model.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(Type entityType, string id)
          : this(entityType.Name + " with id=" + id + " was not found.")
        {
        }

        public EntityNotFoundException(string message)
          : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}
