using System;
using System.Net;

namespace Harcourts.Face.Client
{
    public class FaceApiException : Exception
    {
        private readonly HttpStatusCode _statusCode;

        public FaceApiException(HttpStatusCode statusCode, string errorMessage) 
            : base(errorMessage)
        {
            _statusCode = statusCode;
        }

        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }
    }
}
