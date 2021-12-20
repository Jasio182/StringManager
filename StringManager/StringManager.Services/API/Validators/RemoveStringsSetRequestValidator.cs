using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveStringsSetRequestValidator : RequestBaseValidator<RemoveStringsSetRequest, StringsSet>
    {
        public RemoveStringsSetRequestValidator()
        {
            RuleFor(stringSet => stringSet.Id).GreaterThan(0);
            RuleFor(stringSet => stringSet.AccountType).NotNull();
            RuleFor(stringSet => stringSet.UserId).NotNull();
        }
    }
}
