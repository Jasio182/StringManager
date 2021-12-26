using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class ModifyUserRequestValidator : RequestBaseValidator<ModifyUserRequest, User>
    {
        public ModifyUserRequestValidator()
        {
            RuleFor(user => user.Id).GreaterThan(0);
            RuleFor(user => user.DailyMaintanance).IsInEnum();
            RuleFor(user => user.PlayStyle).IsInEnum();
            RuleFor(user => user.AccountTypeToUpdate).IsInEnum();
        }
    }
}
