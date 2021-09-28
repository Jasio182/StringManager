using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddStringInSetRequestValidator : AbstractValidator<AddStringInSetRequest>
    {
        public AddStringInSetRequestValidator()
        {
            RuleFor(stringInSet => stringInSet.Position).NotNull();
            RuleFor(stringInSet => stringInSet.Position).GreaterThan(0);
            RuleFor(stringInSet => stringInSet.StringId).NotNull();
            RuleFor(stringInSet => stringInSet.StringId).GreaterThan(0);
            RuleFor(stringInSet => stringInSet.StringsSetId).NotNull();
            RuleFor(stringInSet => stringInSet.StringsSetId).GreaterThan(0);
        }
    }
}
