using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harcourts.Face.WebsiteCommon.Models;
using MongoDB.Driver;

namespace Harcourts.Face.WebsiteRepository
{
    public class PersonRepository : SingleCollectionRepository<Person>
    {
        public PersonRepository()
            : base("persons")
        {
        }

        public async Task<Person> Get()
        {
            await ExecuteCollection(
                async x =>
                      {
                          using (var cursor = await x.FindAsync(FilterDefinition<Person>.Empty))
                          {
                              while (await cursor.MoveNextAsync())
                              {
                                  var batch = cursor.Current;

                              }
                          }
                          return 1;
                      });

            return null;
        }


        
    }
}
