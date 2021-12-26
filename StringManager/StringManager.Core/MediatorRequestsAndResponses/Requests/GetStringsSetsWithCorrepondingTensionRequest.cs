using StringManager.Core.Enums;
using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetStringsSetsWithCorrepondingTensionRequest : RequestBase<List<StringsSet>>
    {
        public int MyInstrumentId { get; set; }

        public StringType? StringType { get; set; }

        public int ResultTuningId { get; set; }
    }
}
