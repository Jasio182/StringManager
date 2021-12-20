﻿using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class RemoveToneRequestValidator : RequestBaseValidator<RemoveToneRequest, Tone>
    {
        public RemoveToneRequestValidator()
        {
            RuleFor(tone => tone.Id).GreaterThan(0);
            RuleFor(tone => tone.AccountType).NotNull();
            RuleFor(tone => tone.UserId).NotNull();
        }
    }
}
