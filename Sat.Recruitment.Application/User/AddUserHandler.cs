using MediatR;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.User
{
    public sealed class AddUserHandler : IRequestHandler<AddUserRequest, IOperationResult<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public AddUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IOperationResult<UserDto>> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            return BasicOperationResult<UserDto>.Ok();
        }
    }
}
