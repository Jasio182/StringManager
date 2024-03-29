﻿using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;

namespace StringManager.Services.API.Validators
{
    public class GetStringTensionRequestValidator : RequestBaseValidator<GetStringTensionRequest, double?>
    {
        public GetStringTensionRequestValidator()
        {
            RuleFor(request => request.ScaleLenght).GreaterThan(0);
            RuleFor(request => request.StringId).GreaterThan(0);
            RuleFor(request => request.ToneId).GreaterThan(0);
        }
    }
}
