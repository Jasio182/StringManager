using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveStringInSetRequest : RequestBase<RemoveStringInSetResponse>
    {
        public int Id { get; set; }
    }
}
