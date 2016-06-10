using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Harcourts.Face.WebsiteCommon.Models;

namespace Harcourts.Face.WebsiteCommon
{
    /// <summary>
    /// Returns uniformed error message when error occurred.
    /// </summary>
    public sealed class UniformErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ErrorModel error;
            HttpStatusCode status;
            var strongTypeException = actionExecutedContext.Exception as RaiseErrorException;
            if (strongTypeException == null)
            {
                status = HttpStatusCode.InternalServerError;
                error = new ErrorModel
                        {
                            Message = "Whoops! Something has just killed me."
                        };
            }
            else
            {
                status = strongTypeException.Status;
                error = new ErrorModel
                        {
                            Message = strongTypeException.Message
                        };
            }
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(status, error);
        }
    }
}
