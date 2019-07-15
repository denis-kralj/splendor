using System;
using System.Runtime.Serialization;

namespace splendor_lib
{
    [Serializable]
    public class PlayerHandFullException : Exception
    {
        public PlayerHandFullException()
        {
        }

        public PlayerHandFullException(string message) : base(message)
        {
        }

        public PlayerHandFullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PlayerHandFullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}