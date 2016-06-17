using System;

namespace Harcourts.Face.WebsiteRepository
{
    /// <summary>
    /// Represents the exception thrown by a repository.
    /// </summary>
    public class RepositoryException : Exception
    {
        private readonly IMongoDbAccessor _repository;

        /// <summary>
        /// Initializes an instance of the exception.
        /// </summary>
        /// <param name="repository">The repository where the error occurred.</param>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The actual exception occurred.</param>
        internal RepositoryException(IMongoDbAccessor repository, string message, Exception innerException = null)
            : base(message, innerException)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets the repository where the exception occurred.
        /// </summary>
        public IMongoDbAccessor Repository
        {
            get { return _repository; }
        }
    }
}
