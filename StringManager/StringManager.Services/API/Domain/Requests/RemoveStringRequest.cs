using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveStringRequest : RequestBase<RemoveStringResponse>
    {
        public int Id { get; set; }
    }
}
