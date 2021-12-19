﻿using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class AddStringRequestValidator : RequestBaseValidator<AddStringRequest, String>
    {
        public AddStringRequestValidator()
        {
            RuleFor(thisString => thisString.StringType).IsInEnum();
            RuleFor(thisString => thisString.Size).GreaterThan(0);
            RuleFor(thisString => thisString.SpecificWeight).GreaterThan(0);
            RuleFor(thisString => thisString.NumberOfDaysGood).GreaterThan(0);
            RuleFor(thisString => thisString.ManufacturerId).GreaterThan(0);
            RuleFor(stringInSet => stringInSet.AccountType).NotNull();
            RuleFor(stringInSet => stringInSet.UserId).NotNull();
        }
    }
}
