using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Harcourts.Face.WebsiteCommon;
using Harcourts.Face.WebsiteCommon.ApiServicing;
using Harcourts.Face.WebsiteCommon.Models;

namespace Harcourts.Face.WebsiteService.ApiServicing
{
    public class FaceIdentifyService : IFaceIdentifyService, IApiService<FaceIdentifyConfiguration>
    {
        public bool IsDebugging
        {
            get
            {
                if (Configuration == null)
                {
                    throw new InvalidOperationException("No configuration set.");
                }
                return (Configuration.RouteTemplate ?? string.Empty).EndsWith("/debug",
                    StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public FaceIdentifyConfiguration Configuration { get; set; }

        public async Task<IEnumerable<Person>> Identify(Stream imageStream)
        {
            if (Configuration == null)
            {
                throw new InvalidOperationException("No configuration set.");
            }
            if (Configuration.IdentifyServiceProvider == null || Configuration.PersonLookupServiceProvider == null)
            {
                throw new InvalidOperationException("Require all service providers set.");
            }
            
            try
            {
                if (imageStream == null || !imageStream.CanRead)
                {
                    return Enumerable.Empty<Person>();
                }

                var identifyService = Configuration.IdentifyServiceProvider;
                var keys = await identifyService.Identify(imageStream);
                var personLookup = Configuration.PersonLookupServiceProvider;
                var persons = await personLookup.Find(keys);
                if (!persons.Any())
                {
                    throw new RaiseErrorException(HttpStatusCode.NotFound, "Face not detected or person not found.");
                }
                return persons;
            }
            catch (RaiseErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RaiseErrorException(HttpStatusCode.BadRequest, "Could not proceed the request.", ex);
            }
            finally
            {
                if (imageStream != null)
                {
                    imageStream.Close();
                }
            }
        }
    }
}
