using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyTuningRequestValidator : AbstractValidator<ModifyTuningRequest>
    {
        public ModifyTuningRequestValidator()
        {
            RuleFor(tuning => tuning.Id).NotNull();
            RuleFor(tuning => tuning.Id).GreaterThan(0);
        }
    }
}
