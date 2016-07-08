using System;
using System.IO;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.Recognition;

namespace Harcourts.Face.WebsiteService.ApiServicing
{
    public class FaceIdentifyConfiguration : IFaceIdentifyConfiguration
    {
        public IIdentifyServiceProvider<Stream, Guid> IdentifyServiceProvider { get; set; }

        public IPersonLookup<Guid> PersonLookupServiceProvider { get; set; }
        public string RouteTemplate { get; set; }
    }
}
