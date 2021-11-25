using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetTuningsRequest : RequestBase<List<TuningList>>
    {
        public int NumberOfStrings { get; set; }
    }
}
