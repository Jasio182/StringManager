using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveStringInSetRequestValidator : RequestBaseValidator<RemoveStringInSetRequest, StringInSet>
    {
        public RemoveStringInSetRequestValidator()
        {
            RuleFor(stringInSet => stringInSet.Id).GreaterThan(0);
            RuleFor(stringInSet => stringInSet.AccountType).NotNull();
            RuleFor(stringInSet => stringInSet.UserId).NotNull();
        }
    }
}
