using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Harcourts.Face.Website.Models;

namespace Harcourts.Face.WebsiteCommon.Authorization.Http
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class FromTrustedRefererAttribute : AuthorizationFilterAttribute
    {
        private static readonly Lazy<List<Regex>> AllowedHosts = new Lazy<List<Regex>>(LoadAllowedHostFromAppSettings);

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
                    Status = (int) HttpStatusCode.Unauthorized,
                    StatusDescription = "The action has been sepcified to run only if referrer is trusted."
                });
        }

        protected virtual bool IsAuthorized(HttpActionContext actionContext)
        {
            if (!IsFromTrustedReferer(actionContext))
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
                                 new Claim(ClaimTypes.Name, "Trusted Referer")
                             };
                var identity = new ClaimsIdentity(claims, "TrustedReferer");
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

        private static bool IsFromTrustedReferer(HttpActionContext actionContext)
        {
            var referer = actionContext.Request.Headers.Referrer;
            if (referer == null)
            {
                // Visiting without referrer header is not allowed.
                return false;
            }
            return AllowedHosts.Value.Any(allowedHost => allowedHost.IsMatch(referer.Host));
        }

        private static List<Regex> LoadAllowedHostFromAppSettings()
        {
            var list = new List<Regex>();
            var trustedReferers = ConfigurationManager.AppSettings["TrustedReferers"] ?? string.Empty;

            var hosts = trustedReferers.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
#if DEBUG
            hosts = new[] {"localhost"}.Concat(hosts).ToArray();
#endif

            foreach (var host in hosts)
            {
                var pattern = Regex.Escape(host);
                pattern = "^" + pattern.Replace("\\*", ".+").Replace("\\?", ".") + "$";
                list.Add(new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline));
            }

            return list;
        }
    }
}
