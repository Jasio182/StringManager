using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class AddTuningRequestValidator : RequestBaseValidator<AddTuningRequest, Tuning>
    {
        public AddTuningRequestValidator()
        {
            RuleFor(tuning => tuning.Name).NotNull().NotEmpty();
            RuleFor(tuning => tuning.NumberOfStrings).GreaterThan(0);
        }
    }
}
