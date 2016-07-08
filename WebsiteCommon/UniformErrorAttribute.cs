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
            var error = ErrorModel.FromException(actionExecutedContext.Exception);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(error.GetHttpStatusCode(), error);
        }
    }
}
