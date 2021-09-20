using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class GetScaleLenghtsRequestValidator : AbstractValidator<GetScaleLenghtsRequest>
    {
        public GetScaleLenghtsRequestValidator()
        {
            RuleFor(request=> request.InstrumentId).NotNull();
            RuleFor(request => request.InstrumentId).GreaterThan(0);
        }
    }
}
