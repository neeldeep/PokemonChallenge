using System;
using System.Net;

namespace Pokemon.Entities
{
    /// <summary>
    /// This is a custom Exception class. This is used to return custom error message to be user.
    /// </summary>
    public class APIException : Exception
    {
        public HttpStatusCode ErrorCode { get; private set; }
        public APIException(HttpStatusCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
