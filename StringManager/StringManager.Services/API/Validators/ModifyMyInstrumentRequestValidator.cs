using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

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
            RuleFor(myInstrument => myInstrument.GuitarPlace).IsInEnum();
        }
    }
}
