using System;
using System.IO;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.Recognition;

namespace Harcourts.Face.WebsiteService.ApiServicing
{
    public interface IFaceIdentifyConfiguration
    {
        IIdentifyServiceProvider<Stream, Guid> IdentifyServiceProvider { get; }

        IPersonLookup<Guid> PersonLookupServiceProvider { get; }

        string RouteTemplate { get; }
    }
}
