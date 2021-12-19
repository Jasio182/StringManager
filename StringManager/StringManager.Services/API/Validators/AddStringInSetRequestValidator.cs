using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddStringInSetRequestValidator : RequestBaseValidator<AddStringInSetRequest, StringInSet>
    {
        public AddStringInSetRequestValidator()
        {
            RuleFor(stringInSet => stringInSet.Position).NotNull();
            RuleFor(stringInSet => stringInSet.Position).GreaterThan(0);
            RuleFor(stringInSet => stringInSet.StringId).GreaterThan(0);
            RuleFor(stringInSet => stringInSet.StringsSetId).GreaterThan(0);
            RuleFor(stringInSet => stringInSet.AccountType).NotNull();
            RuleFor(stringInSet => stringInSet.UserId).NotNull();
        }
    }
}
