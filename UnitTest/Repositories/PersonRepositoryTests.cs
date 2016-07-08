using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harcourts.Face.UnitTest.Services;
using Harcourts.Face.WebsiteCommon.Models;
using Harcourts.Face.WebsiteRepository;
using Harcourts.Face.WebsiteService.Lookups;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using NUnit.Framework;

namespace Harcourts.Face.UnitTest.Repositories
{
    [TestFixture]
    internal class PersonRepositoryTests
    {
        [Test, Explicit]
        public async void RebuildCollection()
        {
            var lookup = new JsonPersonLookup(new MockDataFileProvider());
            var persons = lookup.AsEnumerable().ToArray();
            var repo = new PersonRepository();

            var batch = new List<WriteModel<Person>>(1024);
            foreach (var person in persons)
            {
                batch.Add(new InsertOneModel<Person>(person));
                if (batch.Count >= 1024)
                {
                    await repo.Execute(async x => await x.BulkWriteAsync(batch));
                    batch.Clear();
                }
            }
            if (batch.Count > 0)
            {
                await repo.Execute(async x => await x.BulkWriteAsync(batch));
                batch.Clear();
            }
        }
    }
}
