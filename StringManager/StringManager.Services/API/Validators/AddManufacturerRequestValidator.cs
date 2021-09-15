using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddManufacturerRequestValidator : AbstractValidator<AddManufacturerRequest>
    {
        public AddManufacturerRequestValidator()
        {
            RuleFor(manufacturer => manufacturer.Name).NotNull();
        }
    }
}
