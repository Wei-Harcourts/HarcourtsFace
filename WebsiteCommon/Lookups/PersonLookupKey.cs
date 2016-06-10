using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harcourts.Face.WebsiteCommon.Lookups
{
    /// <summary>
    /// Key for looking up for a person in the person repository.
    /// </summary>
    /// <typeparam name="TKey">The underlying type of the key data.</typeparam>
    public class PersonLookupKey<TKey>
    {
        private readonly TKey _key;

        /// <summary>
        /// Initializes a person key with underlying key data.
        /// </summary>
        public PersonLookupKey(TKey key)
        {
            if (!typeof (TKey).IsValueType)
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }
            }

            _key = key;
        }

        /// <summary>
        /// The underlying key data.
        /// </summary>
        public TKey Key
        {
            get { return _key; }
        }

        /// <summary>
        /// Gets the hash code of the key.
        /// </summary>
        public override int GetHashCode()
        {
            return _key.GetHashCode();
        }

        /// <summary>
        /// Determines if the object equals to the current key.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            return _key.Equals(((PersonLookupKey<TKey>) obj)._key);
        }
    }
}
