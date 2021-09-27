using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetTuningRequest : RequestBase<GetTuningResponse>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
