using MediatR;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Interfaces;

namespace Sat.Recruitment.Application.User
{
    public sealed class AddUserRequest : IRequest<IOperationResult<UserDto>>
    {
        public AddUserRequest() { }

        public AddUserRequest(string name, string email, string address, string phone, string userType, string money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = money;
        }

        public string Name { get; }
        public string Email { get; }
        public string Address { get; }
        public string Phone { get; }
        public string UserType { get; }
        public string Money { get; }
    }

}
