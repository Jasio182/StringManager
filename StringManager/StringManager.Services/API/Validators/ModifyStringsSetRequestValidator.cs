using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyStringsSetRequestValidator : RequestBaseValidator<ModifyStringsSetRequest, StringsSet>
    {
        public ModifyStringsSetRequestValidator()
        {
            RuleFor(stringSet => stringSet.Id).GreaterThan(0);
            RuleFor(stringSet => stringSet.NumberOfStrings).GreaterThan(0);
            RuleFor(stringSet => stringSet.AccountType).NotNull();
            RuleFor(stringSet => stringSet.UserId).NotNull();
        }
    }
}
