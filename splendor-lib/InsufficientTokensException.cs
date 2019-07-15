using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace splendor_lib
{
    [Serializable]
    public class InsufficientTokensException : Exception
    {
        public IReadOnlyDictionary<Token, int> Price { get; private set; }

        public InsufficientTokensException()
        {
        }

        public InsufficientTokensException(string message) : base(message)
        {
        }

        public InsufficientTokensException(string message, IReadOnlyDictionary<Token, int> price) : base(message)
        {
            this.Price = price;
        }

        public InsufficientTokensException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientTokensException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}