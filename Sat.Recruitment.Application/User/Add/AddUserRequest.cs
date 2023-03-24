using MediatR;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Interfaces;

namespace Sat.Recruitment.Application.User.Add
{
    public sealed class AddUserRequest : IRequest<IOperationResult<UserDto>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public string Money { get; set; }
    }

}
