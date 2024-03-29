﻿using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;

namespace StringManager.Services.API.Validators
{
    public class ModifyInstalledStringRequestValidator : RequestBaseValidator<ModifyInstalledStringRequest, InstalledString>
    {
        public ModifyInstalledStringRequestValidator()
        {
            RuleFor(installedString => installedString.Id).GreaterThan(0);
            RuleFor(installedString => installedString.StringId).GreaterThan(0);
            RuleFor(installedString => installedString.ToneId).GreaterThan(0);
        }
    }
}
