using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddStringRequestValidator : AbstractValidator<AddStringRequest>
    {
        public AddStringRequestValidator()
        {
            RuleFor(thisString => thisString.StringType).NotNull();
            RuleFor(thisString => thisString.StringType).IsInEnum();
            RuleFor(thisString => thisString.Size).NotNull();
            RuleFor(thisString => thisString.Size).GreaterThan(0);
            RuleFor(thisString => thisString.SpecificWeight).NotNull();
            RuleFor(thisString => thisString.SpecificWeight).GreaterThan(0);
            RuleFor(thisString => thisString.NumberOfDaysGood).NotNull();
            RuleFor(thisString => thisString.NumberOfDaysGood).GreaterThan(0);
            RuleFor(thisString => thisString.ManufacturerId).NotNull();
            RuleFor(thisString => thisString.ManufacturerId).GreaterThan(0);
        }
    }
}
