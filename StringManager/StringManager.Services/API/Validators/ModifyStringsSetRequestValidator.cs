﻿using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class ModifyStringsSetRequestValidator : RequestBaseValidator<ModifyStringsSetRequest, StringsSet>
    {
        public ModifyStringsSetRequestValidator()
        {
            RuleFor(stringSet => stringSet.Id).GreaterThan(0);
            RuleFor(stringSet => stringSet.NumberOfStrings).GreaterThan(0);
        }
    }
}
