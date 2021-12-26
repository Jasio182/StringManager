using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class ModifyTuningRequestValidator : RequestBaseValidator<ModifyTuningRequest, Tuning>
    {
        public ModifyTuningRequestValidator()
        {
            RuleFor(tuning => tuning.Id).GreaterThan(0);
            RuleFor(tuning => tuning.NumberOfStrings).GreaterThan(0);
        }
    }
}
