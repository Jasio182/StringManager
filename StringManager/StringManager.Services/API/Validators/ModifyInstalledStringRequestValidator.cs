using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyInstalledStringRequestValidator : RequestBaseValidator<ModifyInstalledStringRequest, InstalledString>
    {
        public ModifyInstalledStringRequestValidator()
        {
            RuleFor(installedString => installedString.Id).GreaterThan(0);
            RuleFor(installedString => installedString.StringId).GreaterThan(0);
            RuleFor(installedString => installedString.ToneId).GreaterThan(0);
            RuleFor(installedString => installedString.AccountType).NotNull();
            RuleFor(installedString => installedString.UserId).NotNull();
        }
    }
}
