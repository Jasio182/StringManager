using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddToneRequestValidator : RequestBaseValidator<AddToneRequest, Tone>
    {
        public AddToneRequestValidator()
        {
            RuleFor(tone => tone.Name).NotNull();
            RuleFor(tone => tone.Frequency).GreaterThan(0);
            RuleFor(tone => tone.WaveLenght).GreaterThan(0);
            RuleFor(tone => tone.AccountType).NotNull();
            RuleFor(tone => tone.UserId).NotNull();
        }
    }
}
