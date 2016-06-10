using System;
using System.Net;

namespace Harcourts.Face.WebsiteCommon
{
    /// <summary>
    /// Raises an error in the application and returns specified HTTP status and formatted error message.
    /// </summary>
    public class RaiseErrorException : Exception
    {
        private readonly HttpStatusCode _status;

        public RaiseErrorException(HttpStatusCode statusCode, string errorMessage)
            : base(errorMessage)
        {
            _status = statusCode;
        }

        /// <summary>
        /// HTTP status of the error.
        /// </summary>
        public HttpStatusCode Status
        {
            get { return _status; }
        }
    }
}
