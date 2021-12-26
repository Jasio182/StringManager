using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class AddStringRequestValidator : RequestBaseValidator<AddStringRequest, String>
    {
        public AddStringRequestValidator()
        {
            RuleFor(thisString => thisString.StringType).IsInEnum();
            RuleFor(thisString => thisString.Size).GreaterThan(0);
            RuleFor(thisString => thisString.SpecificWeight).GreaterThan(0);
            RuleFor(thisString => thisString.NumberOfDaysGood).GreaterThan(0);
            RuleFor(thisString => thisString.ManufacturerId).GreaterThan(0);
        }
    }
}
