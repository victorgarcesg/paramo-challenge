﻿using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Repositories;
using System.Collections.Generic;

namespace Sat.Recruitment.Test.Infrastructure
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public IGenericRepository<User> Users { get; private set; }

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

        public void SaveChanges()
        {
            // do nothing
        }
    }
}
