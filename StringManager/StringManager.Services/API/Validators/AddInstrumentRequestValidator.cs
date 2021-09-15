using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddInstrumentRequestValidator : AbstractValidator<AddInstrumentRequest>
    {
        public AddInstrumentRequestValidator()
        {
            RuleFor(instrument => instrument.NumberOfStrings).GreaterThan(0);
            RuleFor(instrument => instrument.NumberOfStrings).NotNull();
            RuleFor(instrument => instrument.ScaleLenghtTreble).GreaterThan(0);
            RuleFor(instrument => instrument.ScaleLenghtTreble).NotNull();
            RuleFor(instrument => instrument.ScaleLenghtBass).GreaterThan(0);
            RuleFor(instrument => instrument.ScaleLenghtBass).NotNull();
            RuleFor(instrument => instrument.Model).NotNull();
            RuleFor(instrument => instrument.ManufacturerId).NotNull();
        }
    }
}
