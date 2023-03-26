using CsvHelper;
using CsvHelper.Configuration;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Sat.Recruitment.Persistence.Repositories
{
    public class CsvRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CsvConfiguration _csvConfiguration;
        private readonly string _fullPath;
        private List<T> _entities;

        public CsvRepository(FileConfiguration fileConfiguration)
        {
            _fullPath = $"{Directory.GetCurrentDirectory()}{fileConfiguration.Path}";
            _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ",",
                MissingFieldFound = null
            };
        }

        public IOperationResult<T> Create(T entity)
        {
            _entities = ReadCsvFile();
            _entities.Add(entity);

            return BasicOperationResult<T>.Ok(entity);
        }

        public T Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            _entities = ReadCsvFile();
            return _entities.AsQueryable().Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            _entities = ReadCsvFile();
            return _entities.AsQueryable().Where(predicate).ToList();
        }

        public bool Exists(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            _entities = ReadCsvFile();
            return _entities.AsQueryable().Any(predicate);
        }

        public void Save()
        {
            using var writer = new StreamWriter(_fullPath);
            using var csv = new CsvWriter(writer, _csvConfiguration);
            csv.WriteRecords(_entities);
        }

        private List<T> ReadCsvFile()
        {
            using var reader = new StreamReader(_fullPath);
            using var csv = new CsvReader(reader, _csvConfiguration);
            return csv.GetRecords<T>().ToList();
        }
    }
}
