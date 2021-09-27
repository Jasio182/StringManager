using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyUserRequestValidator : AbstractValidator<ModifyUserRequest>
    {
        public ModifyUserRequestValidator()
        {
            RuleFor(user => user.Id).NotNull();
            RuleFor(user => user.Id).GreaterThan(0);
        }
    }
}
