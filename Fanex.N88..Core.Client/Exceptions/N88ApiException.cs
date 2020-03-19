namespace Fanex.N88.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class N88ApiException : Exception, ISerializable
    {
        public N88ApiException()
            : base()
        {
        }

        public N88ApiException(string message)
            : base(message)
        {
        }

        public N88ApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected N88ApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}