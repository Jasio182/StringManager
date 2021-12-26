using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class RemoveStringInSetRequestValidator : RequestBaseValidator<RemoveStringInSetRequest, StringInSet>
    {
        public RemoveStringInSetRequestValidator()
        {
            RuleFor(stringInSet => stringInSet.Id).GreaterThan(0);
        }
    }
}
