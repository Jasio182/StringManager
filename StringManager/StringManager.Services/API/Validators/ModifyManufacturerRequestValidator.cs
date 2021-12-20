using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyManufacturerRequestValidator : RequestBaseValidator<ModifyManufacturerRequest, Manufacturer>
    {
        public ModifyManufacturerRequestValidator()
        {
            RuleFor(manufacturer => manufacturer.Id).GreaterThan(0);
            RuleFor(manufacturer => manufacturer.Name).NotNull();
            RuleFor(manufacturer => manufacturer.Name).NotEmpty();
            RuleFor(manufacturer => manufacturer.AccountType).NotNull();
            RuleFor(manufacturer => manufacturer.UserId).NotNull();
        }
    }
}
