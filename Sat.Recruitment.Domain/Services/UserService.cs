using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Domain.Services
{
    /// <summary>
    /// Implementation of the IUserService interface that calculates the money based on the user type and money value
    /// </summary>
    public sealed class UserService : IUserService
    {
        /// <inheritdoc/>
        public decimal CalculateMoney(User user) => user.UserType switch
        {
            "Normal" => CalculateNormalMoney(user),
            "SuperUser" => CalculateSuperUserMoney(user),
            "Premium" => CalculatePremiumMoney(user),
            _ => 0
        };

        private decimal CalculateNormalMoney(User user)
        {
            decimal money = user.Money;

            decimal percentage = user.Money switch
            {
                decimal x when x > 100 => 0.12m,
                decimal x when x > 10 => 0.8m,
                _ => 0m
            };

            if (percentage > 0)
            {
                money += user.Money * percentage;
            }

            return money;
        }

        private decimal CalculateSuperUserMoney(User user)
        {
            decimal money = user.Money;

            if (user.Money > 100)
            {
                decimal percentage = 0.20m;
                money += user.Money * percentage;
            }

            return money;
        }

        private decimal CalculatePremiumMoney(User user)
        {
            decimal money = user.Money;

            if (user.Money > 100)
            {
                money += user.Money * 2;
            }

            return money;
        }
    }
}
