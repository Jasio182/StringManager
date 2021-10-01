using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveToneInTuningRequestValidator : AbstractValidator<RemoveToneInTuningRequest>
    {
        public RemoveToneInTuningRequestValidator()
        {
            RuleFor(toneInTuning => toneInTuning.Id).NotNull();
            RuleFor(toneInTuning => toneInTuning.Id).GreaterThan(0);
        }
    }
}
