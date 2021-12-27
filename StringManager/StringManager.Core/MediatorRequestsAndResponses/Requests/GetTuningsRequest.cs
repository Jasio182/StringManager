using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetTuningsRequest : RequestBase<List<TuningList>>
    {
        public int? NumberOfStrings { get; set; }
    }
}
