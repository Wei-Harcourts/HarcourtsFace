using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Harcourts.Face.Recognition.ProjectOxford;
using Harcourts.Face.WebsiteCommon.ModelBinding;
using Harcourts.Face.WebsiteCommon.Models;
using Harcourts.Face.WebsiteService.ApiServicing;
using Harcourts.Face.WebsiteService.Lookups;

namespace Harcourts.Face.Website.Controllers
{
    [RoutePrefix("api/face")]
    public class CognitiveServiceController : ActionController
    {
        [Route("status")]
        [HttpGet]
        public async Task<IHttpActionResult> Status()
        {
            await Task.Delay(1);
            return Ok(new {Status = 200});
        }

        [Route("identify", Name = "FaceIdentify")]
        [Route("identify/debug", Name = "FaceIdentifyDebug")]
        [HttpPost, HttpPut]
        public async Task<IEnumerable<Person>> Identify(
            [FromBase64ImageString] Stream imageStream
            )
        {
            var service = ConfigureService<FaceIdentifyService,
                FaceIdentifyConfigurationBase,
                FaceIdentifyConfiguration>(
                    config =>
                    {
                        config.IdentifyServiceProvider = new FaceService();
                        config.PersonLookupServiceProvider = new DbPersonLookup();
                        return new FaceIdentifyConfigurationWrapper(config);
                    });
            var result = await service.Identify(imageStream);
            return result;
        }
    }
}