using AutoMapper;
using MediatR;
using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.User.Add
{
    /// <summary>
    /// Handles the logic for adding a new user.
    /// </summary>
    public sealed class AddUserHandler : IRequestHandler<AddUserRequest, IOperationResult<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance to use.</param>
        /// <param name="mapper">The mapper instance to use.</param>
        /// <param name="userService">The user service instance to use.</param>
        public AddUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// Handles the request to add a new user.
        /// </summary>
        /// <param name="request">The request containing the user information to add.</param>
        /// <param name="cancellationToken">The cancellation token to use.</param>
        /// <returns>The operation result of the action.</returns>
        public async Task<IOperationResult<UserDto>> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var validator = new AddUserRequestValidator();
            FluentValidation.Results.ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Task.FromResult(BasicOperationResult<UserDto>.Fail(errors));
            }

            var user = _mapper.Map<Domain.Entities.User>(request);
            decimal money = _userService.CalculateMoney(user);

            bool exists = _unitOfWork.Users.Exists(x => user.Email == x.Email || user.Phone == x.Phone || user.Address == x.Address || user.Name == x.Name);

            if (exists)
            {
                return await Task.FromResult(BasicOperationResult<UserDto>.Fail("The user is duplicated"));
            }

            _unitOfWork.Users.Create(user);
            _unitOfWork.SaveChanges();

            return await Task.FromResult(BasicOperationResult<UserDto>.Ok());
        }
    }
}
