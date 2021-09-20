using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class GetStringSizeWithCorrepondingTensionRequestValidator : AbstractValidator<GetStringSizeWithCorrepondingTensionRequest>
    {
        public GetStringSizeWithCorrepondingTensionRequestValidator()
        {
            RuleFor(request => request.PrimaryToneId).NotNull();
            RuleFor(request => request.PrimaryToneId).GreaterThan(0);
            RuleFor(request => request.ResultToneId).NotNull();
            RuleFor(request => request.ResultToneId).GreaterThan(0);
            RuleFor(request => request.ScaleLength).NotNull();
            RuleFor(request => request.ScaleLength).GreaterThan(0);
            RuleFor(request => request.StringId).NotNull();
            RuleFor(request => request.StringId).GreaterThan(0);
        }
    }
}
