using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetStringSizeWithCorrepondingTensionRequest : RequestBase<List<String>>
    {
        [FromRoute]
        public int ScaleLength { get; set; }

        [FromRoute]
        public int StringId { get; set; }

        [FromRoute]
        public int PrimaryToneId { get; set; }

        [FromRoute]
        public int ResultToneId { get; set; }
    }
}
