using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiQuasar.Exceptions
{
    public class HttpException: Exception
    {
        public string MessageError { get; set; }

        public HttpStatusCode ErrorCode { get; set; }

        public HttpException()
        {
            MessageError = new string("Internal Server Error");
        }

        public HttpException(HttpStatusCode errorCode = HttpStatusCode.InternalServerError) : this()
        {
            ErrorCode = errorCode;
        }

        public HttpException(string messages)
        {
            MessageError = messages;
        }

        public HttpException(string message, HttpStatusCode errorCode = HttpStatusCode.InternalServerError)
        {
            MessageError = message;
            ErrorCode = errorCode;
        }
    }
}
