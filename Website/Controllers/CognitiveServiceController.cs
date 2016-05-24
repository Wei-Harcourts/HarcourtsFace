using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Harcourts.Face.WebsiteCommon.Authorization.Http;

namespace Harcourts.Face.Website.Controllers
{
    [RoutePrefix("api/face")]
    public class CognitiveServiceController : ApiController
    {
        [Route("status")]
        [HttpGet]
        public async Task<dynamic> Status()
        {
            await Task.Delay(10000);
            return new {Status = 200};
        }
    }
}