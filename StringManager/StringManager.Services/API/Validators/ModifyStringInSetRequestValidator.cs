using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyStringInSetRequestValidator : AbstractValidator<ModifyStringInSetRequest>
    {
        public ModifyStringInSetRequestValidator()
        {
            RuleFor(stringInSet => stringInSet.Id).NotNull();
            RuleFor(stringInSet => stringInSet.Id).GreaterThan(0);
        }
    }
}
