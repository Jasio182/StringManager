using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyMyInstrumentRequestValidator : AbstractValidator<ModifyMyInstrumentRequest>
    {
        public ModifyMyInstrumentRequestValidator()
        {
            RuleFor(myInstrument => myInstrument.Id).NotNull();
            RuleFor(myInstrument => myInstrument.Id).GreaterThan(0);
            RuleFor(myInstrument => myInstrument.LastDeepCleaning).NotNull();
            RuleFor(myInstrument => myInstrument.LastDeepCleaning).LessThanOrEqualTo(System.DateTime.Now);
            RuleFor(myInstrument => myInstrument.LastStringChange).NotNull();
            RuleFor(myInstrument => myInstrument.LastStringChange).LessThanOrEqualTo(System.DateTime.Now);
        }
    }
}
