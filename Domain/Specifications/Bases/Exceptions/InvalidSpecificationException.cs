using System;
using System.Runtime.Serialization;

namespace Domain.Specifications.Bases.Exceptions
{
    [Serializable]
    public sealed class InvalidSpecificationException : Exception
    {
        public InvalidSpecificationException(string message)
            : base(message)
        {
        }

        private InvalidSpecificationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}