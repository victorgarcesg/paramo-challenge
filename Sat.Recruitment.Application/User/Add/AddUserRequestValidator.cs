using FluentValidation;

namespace Sat.Recruitment.Application.User.Add
{
    public sealed class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(request => request.Name).Name();
            RuleFor(request => request.Email).Email();
            RuleFor(request => request.Address).NotEmpty();
            RuleFor(request => request.Phone).Phone();
            RuleFor(request => request.UserType).NotEmpty();
        }
    }
}
