using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveTuningRequestValidator : RequestBaseValidator<RemoveTuningRequest, Tuning>
    {
        public RemoveTuningRequestValidator()
        {
            RuleFor(tuning => tuning.Id).GreaterThan(0);
            RuleFor(tuning => tuning.AccountType).NotNull();
            RuleFor(tuning => tuning.UserId).NotNull(); ;
        }
    }
}
