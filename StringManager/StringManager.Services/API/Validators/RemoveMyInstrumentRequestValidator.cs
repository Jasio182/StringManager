using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveMyInstrumentRequestValidator : AbstractValidator<RemoveMyInstrumentRequest>
    {
        public RemoveMyInstrumentRequestValidator()
        {
            RuleFor(myInstrument => myInstrument.Id).NotNull();
            RuleFor(myInstrument => myInstrument.Id).GreaterThan(0);
        }
    }
}
