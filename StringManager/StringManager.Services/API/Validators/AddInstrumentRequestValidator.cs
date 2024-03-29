﻿using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class AddInstrumentRequestValidator : RequestBaseValidator<AddInstrumentRequest, Instrument>
    {
        public AddInstrumentRequestValidator()
        {
            RuleFor(instrument => instrument.NumberOfStrings).GreaterThan(0);
            RuleFor(instrument => instrument.ScaleLenghtTreble).GreaterThan(0);
            RuleFor(instrument => instrument.ScaleLenghtBass).GreaterThan(0);
            RuleFor(instrument => instrument.Model).NotNull().NotEmpty();
            RuleFor(instrument => instrument.ManufacturerId).GreaterThan(0);
        }
    }
}
