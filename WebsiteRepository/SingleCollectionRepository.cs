using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Harcourts.Face.WebsiteRepository
{
    /// <summary>
    /// Represents a repository of a collection.
    /// </summary>
    /// <typeparam name="TCollection">The type of the item in the collection.</typeparam>
    [DebuggerDisplay("{_collectionName}")]
    public class SingleCollectionRepository<TCollection> : MongoDbRepository
    {
        private readonly IMongoDbAccessor _db;
        private readonly string _collectionName;

        /// <summary>
        /// Initializes an instance of the repository with the specified collection name.
        /// </summary>
        /// <param name="collectionName">The name of the collection.</param>
        public SingleCollectionRepository(string collectionName)
            : this(collectionName, new SingleDbRepository())
        {
        }

        /// <summary>
        /// Initializes an instance of the repository with the specified collection name and database name.
        /// </summary>
        /// <param name="collectionName">The name of the collection.</param>
        /// <param name="dbName">The name of the database.</param>
        public SingleCollectionRepository(string collectionName, string dbName)
            : this(collectionName, new SingleDbRepository(dbName))
        {
        }

        /// <summary>
        /// Initializes an instance of the repository with the specified underlying database accessor.
        /// </summary>
        /// <param name="collectionName">The name of the collection.</param>
        /// <param name="db">The underlying database accessor.</param>
        internal SingleCollectionRepository(string collectionName, IMongoDbAccessor db)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be null or empty.");
            }
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }
            _collectionName = collectionName;
            _db = db;
        }

        /// <summary>
        /// Performs action on the provided database.
        /// </summary>
        /// <param name="dbAction">The action to perform.</param>
        public override async Task<TResult> Execute<TResult>(Func<IMongoDatabase, Task<TResult>> dbAction)
        {
            return await _db.Execute(dbAction);
        }

        /// <summary>
        /// Performs an action on the specified mongodb collection.
        /// </summary>
        /// <typeparam name="TResult">The type of the return result.</typeparam>
        /// <param name="collectionAction">The action to perform.</param>
        public async Task<TResult> ExecuteCollection<TResult>(
            Func<IMongoCollection<TCollection>, Task<TResult>> collectionAction)
        {
            if (collectionAction == null)
            {
                throw new ArgumentNullException("collectionAction");
            }
            try
            {
                var collection = _db.GetCollection<TCollection>(_collectionName);
                var result = await collectionAction.Invoke(collection);
                return result;
            }
            catch (RepositoryException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    this, "Exception occurred when performing database operation.", ex);
            }
        }
    }
}
