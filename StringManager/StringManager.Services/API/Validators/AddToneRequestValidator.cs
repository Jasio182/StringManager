using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddToneRequestValidator : AbstractValidator<AddToneRequest>
    {
        public AddToneRequestValidator()
        {
            RuleFor(tone => tone.Name).NotNull();
            RuleFor(tone => tone.Frequency).NotNull();
            RuleFor(tone => tone.Frequency).GreaterThan(0);
            RuleFor(tone => tone.WaveLenght).NotNull();
            RuleFor(tone => tone.WaveLenght).GreaterThan(0);
        }
    }
}
