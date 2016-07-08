using System;
using System.Net;

namespace Harcourts.Face.WebsiteCommon.Models
{
    /// <summary>
    /// Data returned when error occurs.
    /// </summary>
    public class ErrorModel
    {
        private HttpStatusCode _status;

        public string Message { get; set; }

        public static ErrorModel FromException(Exception ex)
        {
            if (ex == null)
            {
                return null;
            }

            ErrorModel error;
            var strongTypeException = ex as RaiseErrorException;
            if (strongTypeException == null)
            {
                error = new ErrorModel
                        {
                            _status = HttpStatusCode.BadRequest,
                            Message = "Dead Tom."
                        };
            }
            else
            {
                error = strongTypeException.ShowDebugInfo
                    ? new DebugErrorModel {StackTrace = ex.ToString()}
                    : new ErrorModel();
                error._status = strongTypeException.Status;
                error.Message = ex.Message;
            }
            return error;
        }

        public HttpStatusCode GetHttpStatusCode()
        {
            return _status;
        }
    }

    public class DebugErrorModel : ErrorModel
    {
        public string StackTrace { get; set; }
    }
}
