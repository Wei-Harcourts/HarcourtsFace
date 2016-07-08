using System;
using System.Web.Http;
using System.Web.Mvc;
using Harcourts.Face.WebsiteCommon.ApiServicing;

namespace Harcourts.Face.Website.Controllers
{
    /// <summary>
    /// Api action controller.
    /// </summary>
    public abstract class ActionController : ApiController
    {
        /// <summary>
        /// Creates and configures service object for an action.
        /// </summary>
        /// <param name="configuring">Action to configure the service.</param>
        /// <typeparam name="TService">Type of the service.</typeparam>
        /// <typeparam name="TConfiguration">Type of the configuration of the service.</typeparam>
        public TService ConfigureService<TService, TConfiguration>(Action<TConfiguration> configuring)
            where TService : class, IApiService<TConfiguration>
            where TConfiguration : class, IApiServiceConfiguration<TService>
        {
            var container = DependencyResolver.Current;
            var service = (TService) container.GetService(typeof (TService));
            var config = (TConfiguration) container.GetService(typeof (TConfiguration));
            configuring.Invoke(config);
            service.Configuration = config;
            config.SetService(service);
            config.Seal();
            return service;
        }

        /// <summary>
        /// Creates and configures service object for an action via a delegate object.
        /// </summary>
        /// <param name="configuring">Action to configure the service.</param>
        /// <typeparam name="TService">Type of the service.</typeparam>
        /// <typeparam name="TConfiguration">Type of the configuration of the service.</typeparam>
        /// <typeparam name="T">Type of the delegate object to create the configuration of the service.</typeparam>
        public TService ConfigureService<TService, TConfiguration, T>(Func<T, TConfiguration> configuring)
            where TService : class, IApiService<TConfiguration>
            where TConfiguration : IApiServiceConfiguration<TService>
        {
            var container = DependencyResolver.Current;
            var service = (TService) container.GetService(typeof (TService));
            var delegateObj = (T) container.GetService(typeof (T));
            var config = configuring.Invoke(delegateObj);
            service.Configuration = config;
            config.SetService(service);
            config.Seal();
            return service;
        }
    }
}