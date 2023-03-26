using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Repositories;

namespace Sat.Recruitment.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        void SaveChanges();
    }
}
