using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetMyInstrumentRequest : RequestBase<MyInstrument>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
