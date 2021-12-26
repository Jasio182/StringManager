using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class RemoveInstrumentRequestValidator : RequestBaseValidator<RemoveInstrumentRequest, Instrument>
    {
        public RemoveInstrumentRequestValidator()
        {
            RuleFor(instrument => instrument.Id).GreaterThan(0);
            RuleFor(instrument => instrument.AccountType).NotNull();
            RuleFor(instrument => instrument.UserId).NotNull();
        }
    }
}
