using Microsoft.AspNetCore.Mvc;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetMyInstrumentRequest : RequestBase
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
