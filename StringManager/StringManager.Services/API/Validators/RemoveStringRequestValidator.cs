using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveStringRequestValidator : AbstractValidator<RemoveStringRequest>
    {
        public RemoveStringRequestValidator()
        {
            RuleFor(thisString => thisString.Id).NotNull();
            RuleFor(thisString => thisString.Id).GreaterThan(0);
        }
    }
}
