using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Harcourts.Face.WebsiteCommon.Models;

namespace Harcourts.Face.WebsiteService.ApiServicing
{
    public interface IFaceIdentifyService
    {
        Task<IEnumerable<Person>> Identify(Stream imageStream);
    }
}
