using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddMyInstrumentRequestValidator : RequestBaseValidator<AddMyInstrumentRequest, MyInstrument>
    {
        public AddMyInstrumentRequestValidator()
        {
            RuleFor(myInstrument => myInstrument.GuitarPlace).IsInEnum();
            RuleFor(myInstrument => myInstrument.GuitarPlace).NotNull();
            RuleFor(myInstrument => myInstrument.HoursPlayedWeekly).GreaterThanOrEqualTo(0);
            RuleFor(myInstrument => myInstrument.InstrumentId).GreaterThan(0);
            RuleFor(myInstrument => myInstrument.UserId).GreaterThan(0);
            RuleFor(myInstrument => myInstrument.OwnName).NotNull();
            RuleFor(myInstrument => myInstrument.LastDeepCleaning).LessThanOrEqualTo(System.DateTime.Now);
            RuleFor(myInstrument => myInstrument.LastStringChange).LessThanOrEqualTo(System.DateTime.Now);
            RuleFor(manufacturer => manufacturer.AccountType).NotNull();
            RuleFor(manufacturer => manufacturer.UserId).NotNull();
        }
    }
}
