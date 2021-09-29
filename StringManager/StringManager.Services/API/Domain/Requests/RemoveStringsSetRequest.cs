using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveStringsSetRequest : RequestBase<RemoveStringsSetResponse>
    {
        public int Id { get; set; }
    }
}
