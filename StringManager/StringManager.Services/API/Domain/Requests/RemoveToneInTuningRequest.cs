using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveToneInTuningRequest : RequestBase<RemoveToneInTuningResponse>
    {
        public int Id { get; set; }
    }
}
