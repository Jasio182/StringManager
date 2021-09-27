using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(user => user.Username).NotNull();
            RuleFor(user => user.Username).NotEmpty();
            RuleFor(user => user.Password).NotNull();
            RuleFor(user => user.Password).NotEmpty();
            RuleFor(user => user.Email).NotNull();
            RuleFor(user => user.Email).NotEmpty();
            RuleFor(user => user.DailyMaintanance).NotNull();
            RuleFor(user => user.DailyMaintanance).IsInEnum();
            RuleFor(user => user.PlayStyle).NotNull();
            RuleFor(user => user.PlayStyle).IsInEnum();
        }
    }
}
