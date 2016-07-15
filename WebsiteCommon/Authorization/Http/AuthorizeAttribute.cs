using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Harcourts.Face.WebsiteCommon.Models;

namespace Harcourts.Face.WebsiteCommon.Authorization.Http
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthorizeAttribute : AuthorizationFilterAttribute
    {
        private static readonly Lazy<HashSet<string>> TrustedConsumerKeys =
            new Lazy<HashSet<string>>(LoadTrustedConsumerKeys);

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext))
            {
                return;
            }

            if (IsAuthorized(actionContext))
            {
                return;
            }

            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized,
                new ErrorModel
                {
                    Message = "Unauthorized consumer."
                });
        }

        protected virtual bool IsAuthorized(HttpActionContext actionContext)
        {
            var sid = FindTrustedConsumer(actionContext);
            if (string.IsNullOrEmpty(sid))
            {
                return false;
            }

            var currentPrincipal = actionContext.RequestContext.Principal;
            if (currentPrincipal == null || currentPrincipal.Identity == null ||
                !currentPrincipal.Identity.IsAuthenticated)
            {
                // Authenticate with trusted referer claims if not authenticated.
                var claims = new[]
                             {
                                 new Claim(ClaimTypes.Role, "Trusted Consumer"),
                                 new Claim(ClaimTypes.Sid, sid),
                             };
                var identity = new ClaimsIdentity(claims, "TrustedConsumer");
                var principal = new ClaimsPrincipal(identity);
                actionContext.RequestContext.Principal = currentPrincipal = principal;
            }

            return currentPrincipal.Identity.IsAuthenticated;
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return true;
            }

            return actionContext.ControllerContext.ControllerDescriptor
                .GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        private static string FindTrustedConsumer(HttpActionContext actionContext)
        {
            IEnumerable<string> keys;
            if (!actionContext.Request.Headers.TryGetValues("x-whos-the-agent-key", out keys))
            {
                // Visiting without required header is not allowed.
                return string.Empty;
            }
            return keys.FirstOrDefault(key => TrustedConsumerKeys.Value.Contains(key, StringComparer.Ordinal));
        }

        private static HashSet<string> LoadTrustedConsumerKeys()
        {
            var trustedConsumerKeys = ConfigurationManager.AppSettings["TrustedConsumerKeys"] ?? string.Empty;
            var keys = trustedConsumerKeys.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            return new HashSet<string>(keys);
        }
    }
}
