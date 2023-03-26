using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Domain.Contracts
{
    /// <summary>
    /// Represents a service for user-related operations
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Calculates the amount of money for a given user
        /// </summary>
        /// <param name="user">The user object for which to calculate the money</param>
        /// <returns>The calculated amount of money</returns>
        public decimal CalculateMoney(User user);
    }
}
