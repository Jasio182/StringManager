using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Enums;
using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetStringsSetsWithCorrepondingTensionRequest : RequestBase<List<StringsSet>>
    {
        [FromRoute]
        public int MyInstrumentId { get; set; }

        [FromRoute]
        public StringType? StringType { get; set; }

        [FromRoute]
        public int ResultTuningId { get; set; }
    }
}
