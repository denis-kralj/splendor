using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace splendor_lib
{
    [Serializable]
    public class InsufficientTokensException : Exception
    {
        public InsufficientTokensException()
        {
        }

        public InsufficientTokensException(string message) : base(message)
        {
        }

        public InsufficientTokensException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientTokensException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}