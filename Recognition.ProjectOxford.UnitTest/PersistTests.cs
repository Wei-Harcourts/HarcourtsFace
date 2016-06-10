using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Harcourts.Face.Recognition.ProjectOxford.UnitTest
{
    public class PersistTests
    {
        [Test, Explicit]
        public async Task<string> PersistAllExistingNzPersons()
        {
            var persons = await new PersonGroupTests().GetAllNzPersons();
            var json = JsonConvert.SerializeObject(persons,
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
            return json;
        }

        [Test, Explicit]
        public async Task<string> PersistScrawlResults()
        {
            var persons = await new ScrawlTests().ScrawlGrenadierAgents();
            var json = JsonConvert.SerializeObject(persons,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return json;
        }
    }
}
