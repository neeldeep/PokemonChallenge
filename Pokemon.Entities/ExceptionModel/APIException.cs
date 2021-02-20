using System;
using System.Net;

namespace Pokemon.Entities
{
    public class APIException : Exception
    {
        public HttpStatusCode ErrorCode { get; private set; }
        public APIException(HttpStatusCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
