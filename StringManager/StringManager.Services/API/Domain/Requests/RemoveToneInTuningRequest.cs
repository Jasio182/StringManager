using Microsoft.AspNetCore.Mvc;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveToneInTuningRequest : RequestBase
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
