using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddTuningRequestValidator : AbstractValidator<AddTuningRequest>
    {
        public AddTuningRequestValidator()
        {
            RuleFor(tuning => tuning.Name).NotNull();
            RuleFor(tuning => tuning.NumberOfStrings).NotNull();
            RuleFor(tuning => tuning.NumberOfStrings).GreaterThan(0);
        }
    }
}
