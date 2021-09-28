using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveStringInSetRequestValidator : AbstractValidator<RemoveStringInSetRequest>
    {
        public RemoveStringInSetRequestValidator()
        {
            RuleFor(stringInSet => stringInSet.Id).NotNull();
            RuleFor(stringInSet => stringInSet.Id).GreaterThan(0);
        }
    }
}
