using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetMyInstrumentsRequest : RequestBase<List<MyInstrumentList>>
    {
        public int? RequestUserId { get; set; }
    }
}
