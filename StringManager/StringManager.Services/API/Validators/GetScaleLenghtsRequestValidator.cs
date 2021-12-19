using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class GetScaleLenghtsRequestValidator : RequestBaseValidator<GetScaleLenghtsRequest, int[]>
    {
        public GetScaleLenghtsRequestValidator()
        {
            RuleFor(request => request.InstrumentId).GreaterThan(0);
        }
    }
}
