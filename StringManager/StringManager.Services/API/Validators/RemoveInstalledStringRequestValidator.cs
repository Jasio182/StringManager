using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    class RemoveInstalledStringRequestValidator : AbstractValidator<RemoveInstalledStringRequest>
    {
        public RemoveInstalledStringRequestValidator()
        {
            RuleFor(installedString => installedString.Id).NotNull();
            RuleFor(installedString => installedString.Id).GreaterThan(0);
        }
    }
}
