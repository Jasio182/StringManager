using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddInstalledStringRequestValidator : RequestBaseValidator<AddInstalledStringRequest, InstalledString>
    {
        public AddInstalledStringRequestValidator() : base()
        {
            RuleFor(installedString => installedString.Position).GreaterThan(0);
            RuleFor(installedString => installedString.MyInstrumentId).GreaterThan(0);
            RuleFor(installedString => installedString.StringId).GreaterThan(0);
            RuleFor(installedString => installedString.ToneId).GreaterThan(0);
            RuleFor(installedString => installedString.AccountType).NotNull();
            RuleFor(installedString => installedString.UserId).NotNull();
        }
    }
}
