using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using Harcourts.Face.Recognition.ProjectOxford;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.ModelBinding;
using Harcourts.Face.WebsiteService.Lookups;

namespace Harcourts.Face.Website.Controllers
{
    [RoutePrefix("api/face")]
    public class CognitiveServiceController : ApiController
    {
        [Route("status")]
        [HttpGet]
        public async Task<IHttpActionResult> Status()
        {
            await Task.Delay(10000);
            return Ok(new {Status = 200});
        }

        [Route("identify")]
        [HttpPost, HttpPut]
        public async Task<dynamic> Identify(
            [FromBase64ImageString] Stream imageStream
            )
        {
            try
            {
                //var service = new FaceService();
                //var keys = await service.Identify(imageStream);

                var keys = new[]
                           {
                               new PersonLookupKey<Guid>(new Guid("cc406105-8c66-4ebc-a77a-6536db4c9a9c")),
                               new PersonLookupKey<Guid>(new Guid("4843b5f5-b09d-4e77-b9ae-fa65fb874651"))
                           };

                var personLookup = new JsonPersonLookup();
                var persons = keys.SelectMany(key => personLookup.Find(key))
                    .OrderBy(p => p.FullName)
                    .ToList();

            }
            finally
            {
                imageStream.Close();
            }
            return null;
        }
    }
}