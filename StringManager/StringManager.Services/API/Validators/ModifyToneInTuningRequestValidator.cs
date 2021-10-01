using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    class ModifyToneInTuningRequestValidator : AbstractValidator<ModifyToneInTuningRequest>
    {
        public ModifyToneInTuningRequestValidator()
        {
            RuleFor(toneInTuning => toneInTuning.Id).NotNull();
            RuleFor(toneInTuning => toneInTuning.Id).GreaterThan(0);
        }
    }
}
