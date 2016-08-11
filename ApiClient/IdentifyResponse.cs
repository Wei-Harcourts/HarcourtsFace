using System;
using System.Collections.Generic;
using System.Net;

namespace Harcourts.Face.Client
{
    public abstract class IdentifyResponse
    {
        private readonly Exception _error;
        private readonly HttpStatusCode _httpStatusCode;
        private readonly IEnumerable<Person> _data;

        protected IdentifyResponse(HttpStatusCode statusCode, IEnumerable<Person> data, Exception error)
        {
            _httpStatusCode = statusCode;
            _data = data;
            _error = error;
        }

        public abstract bool HasError { get; }

        public Exception Error
        {
            get { return _error; }
        }

        public HttpStatusCode StatusCode
        {
            get { return _httpStatusCode; }
        }

        public IEnumerable<Person> Data
        {
            get { return _data; }
        }

        public static IdentifyResponse CreateSuccessful(IEnumerable<Person> data)
        {
            return new IdentifySuccessfulResponse(data);
        }

        public static IdentifyResponse CreateError(FaceApiException error)
        {
            return new IdentifyErrorResponse(error.StatusCode, error);
        }

        private class IdentifySuccessfulResponse : IdentifyResponse
        {
            public IdentifySuccessfulResponse(IEnumerable<Person> data)
                : base(HttpStatusCode.OK, data, null)
            {
            }

            public override bool HasError
            {
                get { return false; }
            }
        }

        private class IdentifyErrorResponse : IdentifyResponse
        {
            public IdentifyErrorResponse(HttpStatusCode statusCode, Exception error)
                : base(statusCode, null, error)
            {
            }

            public override bool HasError
            {
                get { return true; }
            }
        }
    }
}
