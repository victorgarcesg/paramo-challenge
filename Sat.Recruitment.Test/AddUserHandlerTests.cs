using AutoMapper;
using FluentValidation.Results;
using Moq;
using Sat.Recruitment.Application.User.Add;
using Sat.Recruitment.Domain.Contracts;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Test.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class AddUserHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly AddUserHandler _handler;

        public AddUserHandlerTests()
        {
            var fixture = new TestFixture();
            _unitOfWorkMock = fixture.UnitOfWork;
            _mapper = fixture.Mapper;
            _userServiceMock = new Mock<IUserService>();
            _handler = new AddUserHandler(_unitOfWorkMock, _mapper, _userServiceMock.Object);
        }

        public static IEnumerable<object[]> AddUserRequestTestCases
        {
            get
            {
                yield return new object[] { new AddUserRequest { Name = "Jane Doe", Email = "janedoe@example.com", Address = "456 Elm St", Phone = "+1 555-555-1313", UserType = "User" }};
                yield return new object[] { new AddUserRequest { Name = "Bob Smith", Email = "bobsmith@example.com", Address = "789 Oak Ave", Phone = "+1 555-555-1414", UserType = "Admin" }};
                yield return new object[] { new AddUserRequest { Name = "Mary Johnson", Email = "maryjohnson@example.com", Address = "222 Cherry Ln", Phone = "+1 555-555-1515", UserType = "User" }};
                yield return new object[] { new AddUserRequest { Name = "Mike Brown", Email = "mikebrown@example.com", Address = "333 Maple Rd", Phone = "+1 555-555-1616", UserType = "Admin" }};
            }
        }

        [Theory]
        [MemberData(nameof(AddUserRequestTestCases))]
        public async Task Add_ValidRequest_ReturnsSuccessResult(AddUserRequest request)
        {
            // Arrange
            var expectedResult = BasicOperationResult<UserDto>.Ok();

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Equal(expectedResult.Success, result.Success);
        }

        [Fact]
        public async Task Add_InvalidRequest_ReturnsErrorResult()
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

        [Fact]
        public async Task Add_DuplicatedUser_ReturnsErrorResult()
        {
            // Arrange
            var request = new AddUserRequest
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "124"
            };
            var expectedResult = BasicOperationResult<UserDto>.Fail("The user is duplicated");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(expectedResult.Messages, result.Messages);
        }
    }
}
