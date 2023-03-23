using FluentValidation;

namespace Sat.Recruitment.Application.User
{
    public sealed class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(request => request.Name).Name();
            RuleFor(request => request.Email).Email();
            RuleFor(request => request.Address).Address();
            RuleFor(request => request.Phone).Phone();
        }
    }
}
