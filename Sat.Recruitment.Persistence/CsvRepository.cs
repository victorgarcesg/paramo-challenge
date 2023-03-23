using CsvHelper;
using CsvHelper.Configuration;
using Sat.Recruitment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Sat.Recruitment.Persistence
{
    public class CsvRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly string _filePath;
        private readonly CsvConfiguration _csvConfiguration;

        public CsvRepository(string filePath)
        {
            _filePath = filePath;
            _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };
        }

        public T Create(T entity)
        {
            List<T> entities = ReadCsvFile();
            entities.Add(entity);
            WriteCsvFile(entities);

            return entity;
        }

        public T Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            List<T> entities = ReadCsvFile();
            return entities.AsQueryable().Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            List<T> entities = ReadCsvFile();
            return entities.AsQueryable().Where(predicate).ToList();
        }

        public bool Exists(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            List<T> entities = ReadCsvFile();
            return entities.AsQueryable().Any(predicate);
        }

        public void Save()
        {
            // No implementation needed for CSV
        }

        private List<T> ReadCsvFile()
        {
            using var reader = new StreamReader(_filePath);
            using var csv = new CsvReader(reader, _csvConfiguration);
            return csv.GetRecords<T>().ToList();
        }

        private void WriteCsvFile(List<T> entities)
        {
            using var writer = new StreamWriter(_filePath);
            using var csv = new CsvWriter(writer, _csvConfiguration);
            csv.WriteRecords(entities);
        }
    }
}
