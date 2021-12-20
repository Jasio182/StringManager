﻿using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public class ModifyToneInTuningRequestValidator : RequestBaseValidator<ModifyToneInTuningRequest, ToneInTuning>
    {
        public ModifyToneInTuningRequestValidator()
        {
            RuleFor(toneInTuning => toneInTuning.Id).GreaterThan(0);
            RuleFor(toneInTuning => toneInTuning.Position).GreaterThan(0);
            RuleFor(toneInTuning => toneInTuning.ToneId).GreaterThan(0);
            RuleFor(toneInTuning => toneInTuning.TuningId).GreaterThan(0);
            RuleFor(toneInTuning => toneInTuning.AccountType).NotNull();
            RuleFor(toneInTuning => toneInTuning.UserId).NotNull();
        }
    }
}
