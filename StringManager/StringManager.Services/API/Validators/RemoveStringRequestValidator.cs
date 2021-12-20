﻿using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveStringRequestValidator : RequestBaseValidator<RemoveStringRequest, String>
    {
        public RemoveStringRequestValidator()
        {
            RuleFor(thisString => thisString.Id).GreaterThan(0);
            RuleFor(thisString => thisString.AccountType).NotNull();
            RuleFor(thisString => thisString.UserId).NotNull();
        }
    }
}
