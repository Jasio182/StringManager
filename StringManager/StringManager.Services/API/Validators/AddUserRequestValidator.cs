using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class AddUserRequestValidator : RequestBaseValidator<AddUserRequest, User>
    {
        public AddUserRequestValidator()
        {
            RuleFor(user => user.Username).NotNull();
            RuleFor(user => user.Username).NotEmpty();
            RuleFor(user => user.Password).NotNull();
            RuleFor(user => user.Password).NotEmpty();
            RuleFor(user => user.Email).NotNull();
            RuleFor(user => user.Email).NotEmpty();
            RuleFor(user => user.DailyMaintanance).IsInEnum();
            RuleFor(user => user.PlayStyle).IsInEnum();
            RuleFor(user => user.AccountTypeToAdd).IsInEnum();
        }
    }
}
