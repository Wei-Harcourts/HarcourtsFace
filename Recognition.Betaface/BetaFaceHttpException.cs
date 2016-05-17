using System;
using System.Net;
using RestSharp;

namespace Harcourts.Face.Recognition.Betaface
{
    /// <summary>
    /// Exception thrown by <see cref="BetaFaceEngine"/>. See <see cref="BetaFaceHttpException.InnerException"/> for details.
    /// </summary>
    public sealed class BetaFaceHttpException : Exception
    {
        private readonly HttpStatusCode _httpStatusCode;

        /// <summary>
        /// Builds exception from <see cref="IRestResponse"/>.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        public BetaFaceHttpException(IRestResponse response)
            : base(response.ErrorMessage, response.ErrorException)
        {
            _httpStatusCode = response.StatusCode;
        }

        /// <summary>
        /// Gets the HTTP status code of the response.
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get { return _httpStatusCode; }
        }
    }
}
