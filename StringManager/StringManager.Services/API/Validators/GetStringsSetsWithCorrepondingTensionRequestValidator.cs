using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class GetStringsSetsWithCorrepondingTensionRequestValidator : AbstractValidator<GetStringsSetsWithCorrepondingTensionRequest>
    {
        public GetStringsSetsWithCorrepondingTensionRequestValidator()
        {
            RuleFor(request=>request.MyInstrumentId).NotNull();
            RuleFor(request => request.MyInstrumentId).GreaterThan(0);
            RuleFor(request => request.ResultTuningId).NotNull();
            RuleFor(request => request.ResultTuningId).GreaterThan(0);
            RuleFor(request => request.StringType).NotNull();
            RuleFor(request => request.StringType).IsInEnum();
        }
    }
}
