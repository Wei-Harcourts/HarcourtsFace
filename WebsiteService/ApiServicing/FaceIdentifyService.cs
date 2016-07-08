using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Harcourts.Face.WebsiteCommon;
using Harcourts.Face.WebsiteCommon.ApiServicing;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.Models;
using Harcourts.Face.WebsiteService.Lookups;

namespace Harcourts.Face.WebsiteService.ApiServicing
{
    public class FaceIdentifyService : IFaceIdentifyService, IApiService<FaceIdentifyConfigurationBase>
    {
        public bool IsDebugging
        {
            get
            {
                return (Configuration.RouteTemplate ?? string.Empty).EndsWith("/debug",
                    StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public FaceIdentifyConfigurationBase Configuration { get; set; }

        public async Task<IEnumerable<Person>> Identify(Stream imageStream)
        {
            try
            {
                if (imageStream == null || !imageStream.CanRead)
                {
                    return Enumerable.Empty<Person>();
                }

                var keys = new[]
                           {
                               new PersonLookupKey<Guid>(new Guid("cc406105-8c66-4ebc-a77a-6536db4c9a9c")),
                               new PersonLookupKey<Guid>(new Guid("4843b5f5-b09d-4e77-b9ae-fa65fb874651"))
                           };
                //var service = new FaceService();
                //var keys = await service.Identify(imageStream);

                //var personLookup = new JsonPersonLookup();
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
