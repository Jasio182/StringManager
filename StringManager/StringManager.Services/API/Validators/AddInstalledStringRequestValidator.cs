using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

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
        }
    }
}
