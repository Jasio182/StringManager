﻿using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class AddStringsSetRequestValidator : RequestBaseValidator<AddStringsSetRequest, StringsSet>
    {
        public AddStringsSetRequestValidator()
        {
            RuleFor(stringSet => stringSet.Name).NotNull();
            RuleFor(stringSet => stringSet.NumberOfStrings).GreaterThan(0);
            RuleFor(stringSet => stringSet.AccountType).NotNull();
            RuleFor(stringSet => stringSet.UserId).NotNull();
        }
    }
}
