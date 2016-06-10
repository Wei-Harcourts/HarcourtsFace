using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Web;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.Models;
using Harcourts.Face.WebsiteService.Mapping;
using Harcourts.Face.WebsiteService.Models;
using Newtonsoft.Json.Linq;

namespace Harcourts.Face.WebsiteService.Lookups
{
    /// <summary>
    /// Implements the person lookup by searching the persisted JSON files.
    /// </summary>
    public sealed class JsonPersonLookup : IPersonLookup<Guid>
    {
        private static readonly Lazy<ILookup<Guid, Person>> PersonProjectionLookup =
            new Lazy<ILookup<Guid, Person>>(BuildPersonLookup, true);

        /// <summary>
        /// Finds the person with the specified person ID.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        public IEnumerable<Person> Find(PersonLookupKey<Guid> personId)
        {
            Contract.Ensures(Contract.Result<IEnumerable<Person>>() != null);

            if (personId == null)
            {
                throw new ArgumentNullException("key");
            }
            if (personId.Key == null)
            {
                throw new ArgumentException("Underlying key is null.");
            }

            var projectionLookup = PersonProjectionLookup.Value;
            var projection = projectionLookup.Contains(personId.Key)
                ? projectionLookup[personId.Key]
                : Enumerable.Empty<Person>();
            return projection;
        }

        /// <summary>
        /// Reads the JSON file and return all existing persons.
        /// </summary>
        private static IReadOnlyDictionary<string, dynamic> ReadExistingPersons()
        {
            var dictionary = ReadJsonDataFile("existingPersons.json")
                .ToDictionary(x => (string) x.userData)
                .AsReadOnly()
                ;
            return dictionary;
        }

        /// <summary>
        /// Reads the JSON file and return all current scrawl results.
        /// </summary>
        private static IReadOnlyDictionary<string, dynamic> ReadScrawlResults()
        {
            var dictionary = ReadJsonDataFile("scrawlResult.json")
                .ToDictionary(x => (string) x.emailAddress)
                .AsReadOnly()
                ;
            return dictionary;
        }

        /// <summary>
        /// Reads in persisted JSON file.
        /// </summary>
        /// <param name="relativeFilePath">The file path relative to the App_Data folder.</param>
        private static IEnumerable<dynamic> ReadJsonDataFile(string relativeFilePath)
        {
            var filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"),
                relativeFilePath.TrimStart('/'));
            var json = File.ReadAllText(filePath);
            var array = JArray.Parse(json);
            var items = array.Children<JObject>()
                .Select(x => x.ToObject<dynamic>())
                .ToList()
                .AsReadOnly()
                ;
            return items;
        }

        /// <summary>
        /// Projects the stored data and generates a lookup for person.
        /// </summary>
        private static ILookup<Guid, Person> BuildPersonLookup()
        {
            var scrawlResults = ReadScrawlResults();
            var existingPersons = ReadExistingPersons();

            var pocoMapper = new ScrawlResultPersonMapper();
            var projectionLookup = scrawlResults.Values
                .Join(existingPersons.Values, r => r.emailAddress, p => p.userData,
                    (r, p) => new {r, p})
                .ToLookup(
                    x => new Guid((string) x.p.personId),
                    x => new Person(pocoMapper.Map(x.r, new PersonPoco())))
                ;

            return projectionLookup;
        }
    }
}
