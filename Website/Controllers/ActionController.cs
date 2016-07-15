using System;
using System.Web.Http;
using System.Web.Mvc;
using Harcourts.Face.WebsiteCommon.ApiServicing;

namespace Harcourts.Face.Website.Controllers
{
    /// <summary>
    /// Api action controller.
    /// </summary>
    public abstract class ActionController<TService, TConfiguration> : ApiController
        where TService : class, IApiService<TConfiguration>
        where TConfiguration : class, IApiServiceConfiguration
    {
        private TService _service;

        /// <summary>
        /// Configures the service object with the settings.
        /// </summary>
        /// <param name="config">The configuring action.</param>
        protected TService ConfigureService(Action<TConfiguration> config)
        {
            var resolver = DependencyResolver.Current;
            if (resolver == null)
            {
                return null;
            }

            if (_service == null)
            {
                _service = resolver.GetService<TService>();
            }

            var configuration = resolver.GetService<TConfiguration>();
            config.Invoke(configuration);
            configuration.Seal();
            _service.Configuration = configuration;
            return _service;
        }
    }
}