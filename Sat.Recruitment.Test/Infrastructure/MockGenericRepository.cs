using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sat.Recruitment.Test.Infrastructure
{
    public class MockGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly List<T> _data;

        public MockGenericRepository(List<T> data)
        {
            _data = data;
        }

        public IOperationResult<T> Create(T entity)
        {
            _data.Add(entity);
            return BasicOperationResult<T>.Ok(entity);
        }

        public T Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _data.AsQueryable();

            return query.FirstOrDefault(predicate);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return _data.Where(predicate.Compile());
        }

        public bool Exists(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _data.AsQueryable();

            return query.Any(predicate);
        }

        public void Save()
        {
            // do nothing, this is a mock
        }
    }
}
