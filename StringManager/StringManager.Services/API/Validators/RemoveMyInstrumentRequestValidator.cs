using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class RemoveMyInstrumentRequestValidator : RequestBaseValidator<RemoveMyInstrumentRequest, MyInstrument>
    {
        public RemoveMyInstrumentRequestValidator()
        {
            RuleFor(myInstrument => myInstrument.Id).GreaterThan(0);
        }
    }
}
