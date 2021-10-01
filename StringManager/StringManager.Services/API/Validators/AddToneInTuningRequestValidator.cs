using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddToneInTuningRequestValidator : AbstractValidator<AddToneInTuningRequest>
    {
        public AddToneInTuningRequestValidator()
        {
            RuleFor(toneInTuning => toneInTuning.Position).NotNull();
            RuleFor(toneInTuning => toneInTuning.Position).GreaterThan(0);
            RuleFor(toneInTuning => toneInTuning.ToneId).NotNull();
            RuleFor(toneInTuning => toneInTuning.ToneId).GreaterThan(0);
            RuleFor(toneInTuning => toneInTuning.TuningId).NotNull();
            RuleFor(toneInTuning => toneInTuning.TuningId).GreaterThan(0);
        }
    }
}
