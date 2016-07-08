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
        /// Finds persons by the specified keys.
        /// </summary>
        /// <param name="keys">The person key.</param>
        Task<IEnumerable<Person>> Find(IEnumerable<PersonLookupKey<TKey>> keys);
    }
}
