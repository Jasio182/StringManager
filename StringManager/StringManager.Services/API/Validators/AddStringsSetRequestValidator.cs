using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddStringsSetRequestValidator : AbstractValidator<AddStringsSetRequest>
    {
        public AddStringsSetRequestValidator()
        {
            RuleFor(stringSet => stringSet.Name).NotNull();
            RuleFor(stringSet => stringSet.NumberOfStrings).NotNull();
            RuleFor(stringSet => stringSet.NumberOfStrings).GreaterThan(0);
        }
    }
}
