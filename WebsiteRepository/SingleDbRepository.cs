using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Harcourts.Face.WebsiteRepository
{
    /// <summary>
    /// Represents a repository in a single database.
    /// </summary>
    [DebuggerDisplay("{_dbName}")]
    public class SingleDbRepository : MongoDbRepository
    {
        private readonly string _dbName;

        /// <summary>
        /// Initializes an instance of the respository with the database name specified in the URI.
        /// </summary>
        public SingleDbRepository()
            : this(MongoUri.DatabaseName)
        {
        }

        /// <summary>
        /// Initializes an instance of the respository with the database name.
        /// </summary>
        /// <param name="dbName">The name of the database.</param>
        public SingleDbRepository(string dbName)
        {
            _dbName = dbName;
        }

        /// <summary>
        /// Performs action on the provided database.
        /// </summary>
        /// <param name="dbAction">The action to perform.</param>
        public override async Task<TResult> RunCommand<TResult>(Func<IMongoDatabase, Task<TResult>> dbAction)
        {
            if (dbAction == null)
            {
                throw new ArgumentNullException("dbAction");
            }
            try
            {
                var client = new MongoClient(MongoUri);
                var db = client.GetDatabase(_dbName);
                var result = await dbAction.Invoke(db);
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
