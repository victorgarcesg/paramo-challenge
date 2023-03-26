using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Repositories;

namespace Sat.Recruitment.Domain.Contracts
{
    /// <summary>
    /// Represents a Unit of Work pattern implementation for the application's data access layer
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Represents a generic repository for User entity in the application's data access layer
        /// </summary>
        IGenericRepository<User> Users { get; }

        /// <summary>
        /// Saves all changes made in this unit of work to the underlying database
        /// </summary>
        void SaveChanges();
    }
}
