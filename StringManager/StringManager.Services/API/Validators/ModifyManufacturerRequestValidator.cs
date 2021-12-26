using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class ModifyManufacturerRequestValidator : RequestBaseValidator<ModifyManufacturerRequest, Manufacturer>
    {
        public ModifyManufacturerRequestValidator()
        {
            RuleFor(manufacturer => manufacturer.Id).GreaterThan(0);
            RuleFor(manufacturer => manufacturer.Name).NotNull().NotEmpty();
        }
    }
}
