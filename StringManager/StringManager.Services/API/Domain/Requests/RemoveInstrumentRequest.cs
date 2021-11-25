using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveInstrumentRequest : RequestBase<Instrument>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
