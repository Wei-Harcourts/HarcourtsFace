using System;
using System.Configuration;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Harcourts.Face.WebsiteRepository
{
    /// <summary>
    /// Represents a repository that uses mongodb as the underlying database.
    /// </summary>
    public abstract class MongoDbRepository : IMongoDbAccessor
    {
        /// <summary>
        /// URI to mongodb.
        /// </summary>
        public static readonly MongoUrl MongoUri;

        /// <summary>
        /// Type constructor.
        /// </summary>
        static MongoDbRepository()
        {
            MongoUri = new MongoUrl(ConfigurationManager.AppSettings["MONGOLAB_URI"]);
        }

        /// <summary>
        /// Performs action on the provided database.
        /// </summary>
        /// <param name="dbAction">The action to perform.</param>
        public abstract Task<TResult> RunCommand<TResult>(Func<IMongoDatabase, Task<TResult>> dbAction);
    }
}
