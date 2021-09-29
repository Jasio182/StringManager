using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyStringsSetRequestValidator : AbstractValidator<ModifyStringsSetRequest>
    {
        public ModifyStringsSetRequestValidator()
        {
            RuleFor(stringSet => stringSet.Id).NotNull();
            RuleFor(stringSet => stringSet.Id).GreaterThan(0);
        }
    }
}
