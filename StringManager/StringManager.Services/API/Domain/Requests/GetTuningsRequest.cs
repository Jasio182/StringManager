using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetTuningsRequest : RequestBase<GetTuningsResponse>
    {
        public int NumberOfStrings { get; set; }
    }
}
