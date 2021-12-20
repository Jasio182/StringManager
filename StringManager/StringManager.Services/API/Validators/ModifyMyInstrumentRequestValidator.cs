using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyMyInstrumentRequestValidator : RequestBaseValidator<ModifyMyInstrumentRequest, MyInstrument>
    {
        public ModifyMyInstrumentRequestValidator()
        {
            RuleFor(myInstrument => myInstrument.Id).GreaterThan(0);
            RuleFor(myInstrument => myInstrument.LastDeepCleaning).LessThanOrEqualTo(System.DateTime.Now);
            RuleFor(myInstrument => myInstrument.LastStringChange).LessThanOrEqualTo(System.DateTime.Now);
            RuleFor(myInstrument => myInstrument.HoursPlayedWeekly).GreaterThanOrEqualTo(0);
            RuleFor(myInstrument => myInstrument.AccountType).NotNull();
            RuleFor(myInstrument => myInstrument.UserId).NotNull();
        }
    }
}
