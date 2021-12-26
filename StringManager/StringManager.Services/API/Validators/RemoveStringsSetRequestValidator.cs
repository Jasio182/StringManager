using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class RemoveStringsSetRequestValidator : RequestBaseValidator<RemoveStringsSetRequest, StringsSet>
    {
        public RemoveStringsSetRequestValidator()
        {
            RuleFor(stringSet => stringSet.Id).GreaterThan(0);
        }
    }
}
