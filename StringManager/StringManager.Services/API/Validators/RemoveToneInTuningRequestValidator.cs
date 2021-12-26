using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class RemoveToneInTuningRequestValidator : RequestBaseValidator<RemoveToneInTuningRequest, ToneInTuning>
    {
        public RemoveToneInTuningRequestValidator()
        {
            RuleFor(toneInTuning => toneInTuning.Id).GreaterThan(0);
        }
    }
}
