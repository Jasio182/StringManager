﻿using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class ModifyStringInSetRequestValidator : RequestBaseValidator<ModifyStringInSetRequest, StringInSet>
    {
        public ModifyStringInSetRequestValidator()
        {
            RuleFor(stringInSet => stringInSet.Id).GreaterThan(0);
            RuleFor(stringInSet => stringInSet.Position).GreaterThan(0);
            RuleFor(stringInSet => stringInSet.StringId).GreaterThan(0);
            RuleFor(stringInSet => stringInSet.StringsSetId).GreaterThan(0);
        }
    }
}
