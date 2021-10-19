using System;
using System.Collections.Generic;
using System.Net;

namespace Employee.Portal.CoreLib.Exceptions
{
    public class AppCustomException : Exception
    {
        public HttpStatusCode HttpCode { get; }
        public List<string> Errors { get; }

        public AppCustomException(HttpStatusCode httpCode, List<string> errors = null)
        {
            Errors = errors;
            HttpCode = httpCode;
        }
    }
}
