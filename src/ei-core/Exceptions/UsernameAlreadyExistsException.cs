using System;
using System.Runtime.Serialization;

namespace ei_core.Exceptions
{
    public class UsernameAlreadyExistsException : Exception
    {
        protected UsernameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UsernameAlreadyExistsException(string duplicatedUsername) : base(
            $"The username \"{duplicatedUsername}\" already exists.")
        {

        }

        public UsernameAlreadyExistsException(string duplicatedUsername, Exception innerException) : base(
            $"The username \"{duplicatedUsername}\" already exists.", innerException)
        {
        }
    }
}