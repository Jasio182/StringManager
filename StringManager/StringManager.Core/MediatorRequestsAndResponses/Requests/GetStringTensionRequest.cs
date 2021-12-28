using Microsoft.AspNetCore.Mvc;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetStringTensionRequest : RequestBase<double?>
    {
        [FromRoute]
        public int StringId { get; set; }

        [FromRoute]
        public int ToneId { get; set; }

        [FromRoute]
        public int ScaleLenght { get; set; }
    }
}
