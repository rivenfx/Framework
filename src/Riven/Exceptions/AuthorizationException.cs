using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Riven.Exceptions
{
    [Serializable]
    public class AuthorizationException : Exception, IAuthorizationException
    {
        public int? Code { get; set; }

        /// <summary>
        /// Creates a new <see cref="AuthorizationException"/> object.
        /// </summary>
        public AuthorizationException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="AuthorizationException"/> object.
        /// </summary>
        public AuthorizationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public AuthorizationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public AuthorizationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}