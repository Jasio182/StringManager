using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

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
            RuleFor(myInstrument => myInstrument.LastDeepCleaning).LessThanOrEqualTo(System.DateTime.Now);
            RuleFor(myInstrument => myInstrument.LastStringChange).LessThanOrEqualTo(System.DateTime.Now);
        }
    }
}
