using System;
using System.IO;
using System.Threading.Tasks;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.Recognition;

namespace Harcourts.Face.Recognition.ProjectOxford
{
    public class DummyFaceService : IIdentifyServiceProvider<Stream, Guid>
    {
        public Task<PersonLookupKey<Guid>[]> Identify(Stream image)
        {
            return Task.FromResult(
                new[]
                {
                    new PersonLookupKey<Guid>(new Guid("cc406105-8c66-4ebc-a77a-6536db4c9a9c")),
                    new PersonLookupKey<Guid>(new Guid("4843b5f5-b09d-4e77-b9ae-fa65fb874651"))
                });
        }
    }
}
