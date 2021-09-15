using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddInstalledStringRequestValidator : AbstractValidator<AddInstalledStringRequest>
    {
        public AddInstalledStringRequestValidator()
        {
            RuleFor(installedString => installedString.Position).GreaterThan(0);
            RuleFor(installedString => installedString.Position).NotNull();
            RuleFor(installedString => installedString.MyInstrumentId).NotNull();
            RuleFor(installedString => installedString.StringId).NotNull();
            RuleFor(installedString => installedString.ToneId).NotNull();
        }
    }
}
