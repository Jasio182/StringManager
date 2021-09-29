using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveStringsSetRequestValidator : AbstractValidator<RemoveStringsSetRequest>
    {
        public RemoveStringsSetRequestValidator()
        {
            RuleFor(stringSet => stringSet.Id).NotNull();
            RuleFor(stringSet => stringSet.Id).GreaterThan(0);
        }
    }
}
