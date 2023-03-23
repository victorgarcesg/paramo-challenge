using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Repositories;

namespace Sat.Recruitment.Persistence.User
{
    public class UserRepository : CsvRepository<Domain.Entities.User>, IUserRepository
    {
        public UserRepository(FileConfiguration fileConfiguration) : base(fileConfiguration) { }
    }
}
