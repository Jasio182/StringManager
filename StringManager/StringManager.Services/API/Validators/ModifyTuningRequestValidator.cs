using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyTuningRequestValidator : RequestBaseValidator<ModifyTuningRequest, Tuning>
    {
        public ModifyTuningRequestValidator()
        {
            RuleFor(tuning => tuning.Id).GreaterThan(0);
            RuleFor(tuning => tuning.NumberOfStrings).GreaterThan(0);
            RuleFor(tuning => tuning.AccountType).NotNull();
            RuleFor(tuning => tuning.UserId).NotNull();
        }
    }
}
