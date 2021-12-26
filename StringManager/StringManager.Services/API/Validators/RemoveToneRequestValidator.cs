using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class RemoveToneRequestValidator : RequestBaseValidator<RemoveToneRequest, Tone>
    {
        public RemoveToneRequestValidator()
        {
            RuleFor(tone => tone.Id).GreaterThan(0);
        }
    }
}
