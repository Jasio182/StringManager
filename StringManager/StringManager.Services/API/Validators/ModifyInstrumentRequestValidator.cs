using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyInstrumentRequestValidator : RequestBaseValidator<ModifyInstrumentRequest, Instrument>
    {
        public ModifyInstrumentRequestValidator()
        {
            RuleFor(instrument => instrument.Id).GreaterThan(0);
            RuleFor(instrument => instrument.NumberOfStrings).GreaterThan(0);
            RuleFor(instrument => instrument.ScaleLenghtBass).GreaterThan(0);
            RuleFor(instrument => instrument.ManufacturerId).GreaterThan(0);
            RuleFor(instrument => instrument.ScaleLenghtTreble).GreaterThan(0);
            RuleFor(instrument => instrument.AccountType).NotNull();
            RuleFor(instrument => instrument.UserId).NotNull();
        }
    }
}
