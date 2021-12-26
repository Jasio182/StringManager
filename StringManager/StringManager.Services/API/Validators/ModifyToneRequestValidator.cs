using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class ModifyToneRequestValidator : RequestBaseValidator<ModifyToneRequest, Tone>
    {
        public ModifyToneRequestValidator()
        {
            RuleFor(tone => tone.Id).GreaterThan(0);
            RuleFor(tone => tone.Frequency).GreaterThan(0);
            RuleFor(tone => tone.WaveLenght).GreaterThan(0);
            RuleFor(tone => tone.AccountType).NotNull();
            RuleFor(tone => tone.UserId).NotNull();
        }
    }
}
