using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class RemoveTuningRequestValidator : RequestBaseValidator<RemoveTuningRequest, Tuning>
    {
        public RemoveTuningRequestValidator()
        {
            RuleFor(tuning => tuning.Id).GreaterThan(0);
        }
    }
}
