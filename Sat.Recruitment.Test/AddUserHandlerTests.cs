using AutoMapper;
using FluentValidation.Results;
using Moq;
using Sat.Recruitment.Application.User.Add;
using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Test.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class AddUserHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly AddUserHandler _handler;

        public AddUserHandlerTests()
        {
            var fixture = new TestFixture();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapper = fixture.Mapper;
            _userServiceMock = new Mock<IUserService>();
            _handler = new AddUserHandler(_unitOfWorkMock.Object, _mapper, _userServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var request = new AddUserRequest
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Address = "123 Main St",
                Phone = "1234567890",
                UserType = "Normal",
                Money = "100"
            };
            var validationResult = new ValidationResult();
            var user = _mapper.Map<Domain.Entities.User>(request);
            var expectedResult = BasicOperationResult<UserDto>.Ok();

            _userServiceMock.Setup(m => m.CalculateMoney(user)).Returns(100);
            _unitOfWorkMock.Setup(m => m.Users.Exists(It.IsAny<Expression<Func<Domain.Entities.User, bool>>>())).Returns(false);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Equal(expectedResult.Success, result.Success);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsErrorResult()
        {
            // Arrange
            var request = new AddUserRequest();
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Name", "'Name' must not be empty."),
                new ValidationFailure("Email", "'Email' must not be empty."),
                new ValidationFailure("Address", "'Address' must not be empty."),
                new ValidationFailure("Phone", "'Phone' must not be empty."),
                new ValidationFailure("UserType", "'User Type' must not be empty.")
            });
            var expectedErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(expectedErrors, result.Messages);
        }
    }
}
