using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harcourts.Face.WebsiteCommon.Models;

namespace Harcourts.Face.WebsiteCommon.Lookups
{
    /// <summary>
    /// Represents a lookup for person.
    /// </summary>
    /// <typeparam name="TKey">The data type of the key.</typeparam>
    public interface IPersonLookup<TKey>
    {
        /// <summary>
        /// Finds a person by the specified key.
        /// </summary>
        /// <param name="key">The person key.</param>
        IEnumerable<Person> Find(PersonLookupKey<TKey> key);
    }
}
