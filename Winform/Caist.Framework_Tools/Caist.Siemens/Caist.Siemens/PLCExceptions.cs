using System;
#if NET_FULL
using System.Runtime.Serialization;    
#endif


namespace Caist.Siemens
{
    internal class WrongNumberOfBytesException : Exception
    {
        public WrongNumberOfBytesException() : base()
        {
        }

        public WrongNumberOfBytesException(string message) : base(message)
        {
        }

        public WrongNumberOfBytesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #if NET_FULL
        protected WrongNumberOfBytesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        #endif
    }

    internal class InvalidAddressException : Exception
    {
        public InvalidAddressException() : base ()
        {
        }

        public InvalidAddressException(string message) : base(message)
        {
        }

        public InvalidAddressException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #if NET_FULL
        protected InvalidAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        #endif
    }

    internal class InvalidVariableTypeException : Exception
    {
        public InvalidVariableTypeException() : base()
        {
        }

        public InvalidVariableTypeException(string message) : base(message)
        {
        }

        public InvalidVariableTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #if NET_FULL
        protected InvalidVariableTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        #endif
    }

    internal class TPKTInvalidException : Exception
    {
        public TPKTInvalidException() : base()
        {
        }

        public TPKTInvalidException(string message) : base(message)
        {
        }

        public TPKTInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #if NET_FULL
        protected TPKTInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        #endif
    }
}
