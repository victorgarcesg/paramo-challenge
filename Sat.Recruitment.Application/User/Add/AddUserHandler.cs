using AutoMapper;
using MediatR;
using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.User.Add
{
    public sealed class AddUserHandler : IRequestHandler<AddUserRequest, IOperationResult<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddUserHandler(IUserRepository userRepository, IMapper mapper, IUserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IOperationResult<UserDto>> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var validator = new AddUserRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Task.FromResult(BasicOperationResult<UserDto>.Fail(errors));
            }

            var user = _mapper.Map<Domain.Entities.User>(request);
            var money = _userService.CalculateMoney(user);

            return await Task.FromResult(BasicOperationResult<UserDto>.Ok());
        }
    }
}
