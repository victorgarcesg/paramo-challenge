using Sat.Recruitment.Domain.Repositories;

namespace Sat.Recruitment.Persistence.User
{
    public class UserRepository : CsvRepository<Domain.Entities.User>, IUserRepository
    {
        public UserRepository(string filePath) : base(filePath) { }
    }
}
