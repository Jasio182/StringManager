using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetStringSizeWithCorrepondingTensionRequest : RequestBase<List<String>>
    {
        public int ScaleLength { get; set; }

        public int StringId { get; set; }

        public int PrimaryToneId { get; set; }

        public int ResultToneId { get; set; }
    }
}
