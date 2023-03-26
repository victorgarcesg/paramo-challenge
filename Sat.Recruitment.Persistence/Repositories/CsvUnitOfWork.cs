using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Repositories;

namespace Sat.Recruitment.Persistence.Repositories
{
    /// <summary>
    /// Represents a unit of work for a CSV data source.
    /// </summary>
    public class CsvUnitOfWork : IUnitOfWork
    {
        private readonly IGenericRepository<User> _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvUnitOfWork"/> class with the specified repository.
        /// </summary>
        /// <param name="userRepository">The repository to use for user data access.</param>
        public CsvUnitOfWork(IGenericRepository<User> userRepository) => _userRepository = userRepository;

        /// <summary>
        /// Gets the repository for user data access.
        /// </summary>
        public IGenericRepository<User> Users { get { return _userRepository; } }

        /// <inheritdoc/>
        public void SaveChanges() => _userRepository.Save();
    }
}
