using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class GetStringTensionRequestValidator : AbstractValidator<GetStringTensionRequest>
    {
        public GetStringTensionRequestValidator()
        {
            RuleFor(request => request.ScaleLenght).NotNull();
            RuleFor(request => request.ScaleLenght).GreaterThan(0);
            RuleFor(request => request.StringId).NotNull();
            RuleFor(request => request.StringId).GreaterThan(0);
            RuleFor(request => request.ToneId).NotNull();
            RuleFor(request => request.ToneId).IsInEnum();
        }
    }
}
