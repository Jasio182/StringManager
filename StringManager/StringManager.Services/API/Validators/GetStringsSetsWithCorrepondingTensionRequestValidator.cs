using FluentValidation;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Services.API.Validators
{
    public class GetStringsSetsWithCorrepondingTensionRequestValidator : RequestBaseValidator<GetStringsSetsWithCorrepondingTensionRequest, List<StringsSet>>
    {
        public GetStringsSetsWithCorrepondingTensionRequestValidator()
        {
            RuleFor(request => request.MyInstrumentId).GreaterThan(0);
            RuleFor(request => request.ResultTuningId).GreaterThan(0);
            RuleFor(request => request.StringType).IsInEnum();
        }
    }
}
