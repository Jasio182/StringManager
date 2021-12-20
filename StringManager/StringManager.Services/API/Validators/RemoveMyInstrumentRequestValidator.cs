using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveMyInstrumentRequestValidator : RequestBaseValidator<RemoveMyInstrumentRequest, MyInstrument>
    {
        public RemoveMyInstrumentRequestValidator()
        {
            RuleFor(myInstrument => myInstrument.Id).GreaterThan(0);
            RuleFor(myInstrument => myInstrument.AccountType).NotNull();
            RuleFor(myInstrument => myInstrument.UserId).NotNull();
        }
    }
}
