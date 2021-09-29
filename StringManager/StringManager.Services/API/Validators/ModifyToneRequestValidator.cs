using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyToneRequestValidator : AbstractValidator<ModifyToneRequest>
    {
        public ModifyToneRequestValidator()
        {
            RuleFor(tone => tone.Id).NotNull();
            RuleFor(tone => tone.Id).GreaterThan(0);
        }
    }
}
