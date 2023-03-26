using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Repositories;

namespace Sat.Recruitment.Persistence.Repositories
{
    public class CsvUnitOfWork : IUnitOfWork
    {
        private readonly IGenericRepository<User> _userRepository;

        public CsvUnitOfWork(IGenericRepository<User> userRepository) => _userRepository = userRepository;

        public IGenericRepository<User> Users { get { return _userRepository; } }

        public void SaveChanges() => _userRepository.Save();
    }
}
