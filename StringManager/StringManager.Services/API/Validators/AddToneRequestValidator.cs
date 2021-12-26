using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class AddToneRequestValidator : RequestBaseValidator<AddToneRequest, Tone>
    {
        public AddToneRequestValidator()
        {
            RuleFor(tone => tone.Name).NotNull().NotEmpty();
            RuleFor(tone => tone.Frequency).GreaterThan(0);
            RuleFor(tone => tone.WaveLenght).GreaterThan(0);
        }
    }
}
