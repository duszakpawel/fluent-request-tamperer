using System;
using System.Runtime.Serialization;

namespace RequestObfuscator.Exceptions
{
    [Serializable]
    public class SerializationException : ApplicationException
    {
        public SerializationException() { }
        public SerializationException(string message) : base(message) { }
        public SerializationException(string message, Exception inner) : base(message, inner) { }
        protected SerializationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
