using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyManufacturerRequestValidator : AbstractValidator<ModifyManufacturerRequest>
    {
        public ModifyManufacturerRequestValidator()
        {
            RuleFor(manufacturer => manufacturer.Id).NotNull();
            RuleFor(manufacturer => manufacturer.Id).GreaterThan(0);
            RuleFor(manufacturer => manufacturer.Name).NotNull();
            RuleFor(manufacturer => manufacturer.Name).NotEmpty();
        }
    }
}
