using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class RemoveInstalledStringRequestValidator : RequestBaseValidator<RemoveInstalledStringRequest, InstalledString>
    {
        public RemoveInstalledStringRequestValidator()
        {
            RuleFor(installedString => installedString.Id).GreaterThan(0);
        }
    }
}
