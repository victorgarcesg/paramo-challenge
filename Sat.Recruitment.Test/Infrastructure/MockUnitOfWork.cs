using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Repositories;
using System.Collections.Generic;

namespace Sat.Recruitment.Test.Infrastructure
{
    /// <summary>
    /// A mock implementation of the <see cref="IUnitOfWork"/> interface, used for testing purposes.
    /// </summary>
    public class MockUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets the <see cref="IGenericRepository{T}"/> for the <see cref="User"/> entity.
        /// </summary>
        public IGenericRepository<User> Users { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockUnitOfWork"/> class.
        /// </summary>
        public MockUnitOfWork()
        {
            var users = new List<User>
            {
                new User { Name = "John Doe", Email = "johndoe@example.com", Address = "123 Main St", Phone = "+1 555-555-1212", UserType = "Admin" },
                new User { Name = "Jane Smith", Email = "janesmith@example.com", Address = "456 Oak St", Phone = "+1 555-555-1212", UserType = "Customer" },
                new User { Name = "Bob Johnson", Email = "bjohnson@example.com", Address = "789 Elm St", Phone = "+1 555-555-1212", UserType = "Customer" },
                new User { Name = "Agustina", Email = "Agustina@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = "Normal" }
            };

            Users = new MockGenericRepository<User>(users);
        }

        /// <summary>
        /// Saves the changes made to the repositories in the unit of work.
        /// </summary>
        public void SaveChanges()
        {
            // do nothing
        }
    }
}
