using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harcourts.Face.WebsiteCommon.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Harcourts.Face.WebsiteRepository
{
    public class PersonRepository : SingleCollectionRepository<Person>
    {
        static PersonRepository()
        {
            BsonClassMap.RegisterClassMap<Person>(
                cm =>
                {
                    cm.SetIgnoreExtraElements(true);
                    cm.SetIgnoreExtraElementsIsInherited(true);

                    cm.MapCreator(p =>
                        new Person(new PersonPoco
                                   {
                                       EmailAddress = p.EmailAddress,
                                       FullName = p.FullName,
                                       PersonIdentity = p.PersonIdentity,
                                       PhotoUri = p.PhotoUri,
                                       ProfileUri = p.ProfileUri,
                                       WorkTitleOrPosition = p.WorkTitleOrPosition
                                   }));

                    cm.MapMember(p => p.FullName).SetElementName("fullName");
                    cm.MapMember(p => p.EmailAddress).SetElementName("emailAddress");
                    cm.MapMember(p => p.PersonIdentity).SetElementName("personIdentity");
                    cm.MapMember(p => p.PhotoUri).SetElementName("photoUri");
                    cm.MapMember(p => p.ProfileUri).SetElementName("profileUri");
                    cm.MapMember(p => p.WorkTitleOrPosition).SetElementName("workTitleOrPosition");
                });
        }

        public PersonRepository()
            : base("persons")
        {
        }

        public async Task<IEnumerable<Person>> Get(Guid[] personIdentitfiers)
        {
            var orList = personIdentitfiers.Select(personIdentifier =>
                Builders<Person>.Filter.Eq(p => p.PersonIdentity, personIdentifier.ToString()));
            var condition = Builders<Person>.Filter.Or(orList);

            return await Execute(
                async persons =>
                      {
                          using (var reader = await persons.FindAsync(condition))
                          {
                              var results = new List<Person>();
                              while (await reader.MoveNextAsync())
                              {
                                  results.AddRange(reader.Current);
                              }
                              return results;
                          }
                      });
        }
    }
}
