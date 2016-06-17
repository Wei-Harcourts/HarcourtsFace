using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Harcourts.Face.WebsiteRepository
{
    /// <summary>
    /// Provides methods to perform actions on the underlying mongodb.
    /// </summary>
    public interface IMongoDbAccessor
    {
        /// <summary>
        /// Performs an action on the specified mongodb.
        /// </summary>
        /// <typeparam name="TResult">The type of the return result.</typeparam>
        /// <param name="dbAction">The action to perform.</param>
        Task<TResult> Execute<TResult>(Func<IMongoDatabase, Task<TResult>> dbAction);
    }
}
