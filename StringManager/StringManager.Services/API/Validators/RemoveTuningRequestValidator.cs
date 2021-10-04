using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveTuningRequestValidator : AbstractValidator<RemoveTuningRequest>
    {
        public RemoveTuningRequestValidator()
        {
            RuleFor(tuning => tuning.Id).NotNull();
            RuleFor(tuning => tuning.Id).GreaterThan(0);
        }
    }
}
