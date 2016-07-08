using System;
using System.IO;
using Harcourts.Face.WebsiteCommon.ApiServicing;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.Recognition;

namespace Harcourts.Face.WebsiteService.ApiServicing
{
    public abstract class FaceIdentifyConfigurationBase : IFaceIdentifyConfiguration, IApiServiceConfiguration<FaceIdentifyService>
    {
        private bool _isSealed;
        private FaceIdentifyService _service;

        public abstract IIdentifyServiceProvider<Stream, Guid> IdentifyServiceProvider { get; }
        public abstract IPersonLookup<Guid> PersonLookupServiceProvider { get; }
        public abstract string RouteTemplate { get; }

        public FaceIdentifyService Service
        {
            get { return _service; }
        }

        public void Seal()
        {
            _isSealed = true;
        }

        void IApiServiceConfiguration<FaceIdentifyService>.SetService(FaceIdentifyService service)
        {
            if (_isSealed)
            {
                throw new InvalidOperationException("Configuration has been sealed.");
            }

            _service = service;
        }
    }
}
