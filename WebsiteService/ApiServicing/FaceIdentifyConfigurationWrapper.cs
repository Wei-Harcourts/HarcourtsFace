using System;
using System.IO;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.Recognition;

namespace Harcourts.Face.WebsiteService.ApiServicing
{
    public class FaceIdentifyConfigurationWrapper : FaceIdentifyConfigurationBase
    {
        private readonly IFaceIdentifyConfiguration _innerConfig;

        public FaceIdentifyConfigurationWrapper(IFaceIdentifyConfiguration config)
        {
            _innerConfig = config;
        }

        public override IIdentifyServiceProvider<Stream, Guid> IdentifyServiceProvider
        {
            get { return _innerConfig.IdentifyServiceProvider; }
        }

        public override IPersonLookup<Guid> PersonLookupServiceProvider
        {
            get { return _innerConfig.PersonLookupServiceProvider; }
        }

        public override string RouteTemplate
        {
            get { return _innerConfig.RouteTemplate; }
        }
    }
}
