using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyInstalledStringRequestValidator : AbstractValidator<ModifyInstalledStringRequest>
    {
        public ModifyInstalledStringRequestValidator()
        {
            RuleFor(installedString => installedString.Id).NotNull();
            RuleFor(installedString => installedString.Id).GreaterThan(0);
        }
    }
}
