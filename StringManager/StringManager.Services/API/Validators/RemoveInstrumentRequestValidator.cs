using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveInstrumentRequestValidator : AbstractValidator<RemoveInstrumentRequest>
    {
        public RemoveInstrumentRequestValidator()
        {
            RuleFor(instrument => instrument.Id).NotNull();
            RuleFor(instrument => instrument.Id).GreaterThan(0);
        }
    }
}
