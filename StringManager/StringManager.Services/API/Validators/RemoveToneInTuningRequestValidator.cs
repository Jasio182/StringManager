using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveToneInTuningRequestValidator : RequestBaseValidator<RemoveToneInTuningRequest, ToneInTuning>
    {
        public RemoveToneInTuningRequestValidator()
        {
            RuleFor(toneInTuning => toneInTuning.Id).GreaterThan(0);
            RuleFor(toneInTuning => toneInTuning.AccountType).NotNull();
            RuleFor(toneInTuning => toneInTuning.UserId).NotNull();
        }
    }
}
