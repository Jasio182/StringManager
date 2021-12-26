using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class RemoveManufacturerRequestValidator : RequestBaseValidator<RemoveManufacturerRequest, Manufacturer>
    {
        public RemoveManufacturerRequestValidator()
        {
            RuleFor(manufacturer => manufacturer.Id).GreaterThan(0);
            RuleFor(manufacturer => manufacturer.AccountType).NotNull();
            RuleFor(manufacturer => manufacturer.UserId).NotNull();
        }
    }
}
