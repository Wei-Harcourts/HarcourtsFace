using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Harcourts.Face.WebsiteRepository
{
    /// <summary>
    /// Generic extension methods for mongodb repositories.
    /// </summary>
    public static class MongoDbRepositoryExtensions
    {
        /// <summary>
        /// Gets the collection with the specified name.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="name">The name of the collection.</param>
        public static IMongoCollection<BsonDocument> GetCollection(this IMongoDbAccessor repository, string name)
        {
            return GetCollection<BsonDocument>(repository, name);
        }

        /// <summary>
        /// Gets the collection with the specified name.
        /// </summary>
        /// <typeparam name="TCollection">The type of the collection item.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="name">The name of the collection.</param>
        public static IMongoCollection<TCollection> GetCollection<TCollection>(
            this IMongoDbAccessor repository, string name)
        {
            if (repository == null)
            {
                throw new ArgumentException("Action cannot perform on current repository.");
            }

            var task = repository.RunCommand(db => Task.FromResult(db.GetCollection<TCollection>(name)));
            return task.Result;
        }
    }
}
