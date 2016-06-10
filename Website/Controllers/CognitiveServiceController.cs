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
                               new PersonLookupKey<Guid>(new Guid("6225b3c2-1ea0-4545-8239-05bc4aef0148")),
                               new PersonLookupKey<Guid>(new Guid("ebf487a5-82e2-4f31-b39f-0fa68892b1d4"))
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