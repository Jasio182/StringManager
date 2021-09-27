using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveManufacturerRequestValidator : AbstractValidator<RemoveManufacturerRequest>
    {
        public RemoveManufacturerRequestValidator()
        {
            RuleFor(manufacturer => manufacturer.Id).NotNull();
            RuleFor(manufacturer => manufacturer.Id).GreaterThan(0);
        }
    }
}
