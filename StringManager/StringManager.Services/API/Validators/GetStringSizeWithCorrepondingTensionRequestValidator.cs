using FluentValidation;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;
using System.Collections.Generic;

namespace StringManager.Services.API.Validators
{
    public class GetStringSizeWithCorrepondingTensionRequestValidator : RequestBaseValidator<GetStringSizeWithCorrepondingTensionRequest, List<String>>
    {
        public GetStringSizeWithCorrepondingTensionRequestValidator()
        {
            RuleFor(request => request.PrimaryToneId).GreaterThan(0);
            RuleFor(request => request.ResultToneId).GreaterThan(0);
            RuleFor(request => request.ScaleLength).GreaterThan(0);
            RuleFor(request => request.StringId).GreaterThan(0);
        }
    }
}
