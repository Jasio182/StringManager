﻿using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class ModifyStringRequestValidator : RequestBaseValidator<ModifyStringRequest, String>
    {
        public ModifyStringRequestValidator()
        {
            RuleFor(stringInSet => stringInSet.Id).GreaterThan(0);
            RuleFor(thisString => thisString.StringType).IsInEnum();
            RuleFor(thisString => thisString.Size).GreaterThan(0);
            RuleFor(thisString => thisString.SpecificWeight).GreaterThan(0);
            RuleFor(thisString => thisString.NumberOfDaysGood).GreaterThan(0);
            RuleFor(thisString => thisString.ManufacturerId).GreaterThan(0);
        }
    }
}
