using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class AddToneInTuningRequestValidator : RequestBaseValidator<AddToneInTuningRequest, ToneInTuning>
    {
        public AddToneInTuningRequestValidator()
        {
            RuleFor(toneInTuning => toneInTuning.Position).GreaterThan(0);
            RuleFor(toneInTuning => toneInTuning.ToneId).GreaterThan(0);
            RuleFor(toneInTuning => toneInTuning.TuningId).GreaterThan(0);
            RuleFor(toneInTuning => toneInTuning.AccountType).NotNull();
            RuleFor(toneInTuning => toneInTuning.UserId).NotNull();
        }
    }
}
