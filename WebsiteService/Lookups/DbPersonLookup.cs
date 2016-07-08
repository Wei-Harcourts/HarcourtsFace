using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.Models;
using Harcourts.Face.WebsiteRepository;

namespace Harcourts.Face.WebsiteService.Lookups
{
    public class DbPersonLookup : IPersonLookup<Guid>
    {
        private readonly PersonRepository _repo;

        public DbPersonLookup()
        {
            _repo = new PersonRepository();
        }

        public async Task<IEnumerable<Person>> Find(IEnumerable<PersonLookupKey<Guid>> keys)
        {
            var identifiers = keys.Select(x => x.Key).ToArray();
            var persons = await _repo.Get(identifiers);
            return persons;
        }
    }
}
