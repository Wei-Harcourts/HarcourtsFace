using System;
using System.IO;
using Harcourts.Face.WebsiteCommon.ApiServicing;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.Recognition;

namespace Harcourts.Face.WebsiteService.ApiServicing
{
    public class FaceIdentifyConfiguration : SealableObject, IApiServiceConfiguration
    {
        public IIdentifyServiceProvider<Stream, Guid> IdentifyServiceProvider
        {
            get { return GetPropertyValue(() => IdentifyServiceProvider); }
            set { SetPropertyValue(() => IdentifyServiceProvider, value); }
        }

        public IPersonLookup<Guid> PersonLookupServiceProvider
        {
            get { return GetPropertyValue(() => PersonLookupServiceProvider); }
            set { SetPropertyValue(() => PersonLookupServiceProvider, value); }
        }

        public string RouteTemplate
        {
            get { return GetPropertyValue(() => RouteTemplate); }
            set { SetPropertyValue(() => RouteTemplate, value); }
        }

        void IApiServiceConfiguration.Seal()
        {
            Seal();
        }
    }
}
