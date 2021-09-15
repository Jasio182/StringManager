using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddMyInstrumentRequestValidator : AbstractValidator<AddMyInstrumentRequest>
    {
        public AddMyInstrumentRequestValidator()
        {
            RuleFor(myInstrument => myInstrument.GuitarPlace).IsInEnum();
            RuleFor(myInstrument => myInstrument.GuitarPlace).NotNull();
            RuleFor(myInstrument => myInstrument.HoursPlayedWeekly).GreaterThanOrEqualTo(0);
            RuleFor(myInstrument => myInstrument.InstrumentId).NotNull();
            RuleFor(myInstrument => myInstrument.UserId).NotNull();
            RuleFor(myInstrument => myInstrument.OwnName).NotNull();
            RuleFor(myInstrument => myInstrument.LastDeepCleaning).NotNull();
            RuleFor(myInstrument => myInstrument.LastDeepCleaning).LessThanOrEqualTo(System.DateTime.Now);
            RuleFor(myInstrument => myInstrument.LastStringChange).NotNull();
            RuleFor(myInstrument => myInstrument.LastStringChange).LessThanOrEqualTo(System.DateTime.Now);
        }
    }
}
