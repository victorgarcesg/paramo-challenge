using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Domain.Contracts
{
    public interface IUserService
    {
        public decimal CalculateMoney(User user);
    }
}
