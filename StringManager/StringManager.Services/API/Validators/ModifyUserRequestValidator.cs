using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyUserRequestValidator : RequestBaseValidator<ModifyUserRequest, User>
    {
        public ModifyUserRequestValidator()
        {
            RuleFor(user => user.Id).NotNull();
            RuleFor(user => user.Id).GreaterThan(0);
            RuleFor(user => user.DailyMaintanance).IsInEnum();
            RuleFor(user => user.PlayStyle).IsInEnum();
            RuleFor(user => user.AccountTypeToUpdate).IsInEnum();
            RuleFor(user => user.AccountType).NotNull();
            RuleFor(user => user.UserId).NotNull();
        }
    }
}
