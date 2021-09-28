using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddStringInSetRequest : RequestBase<AddStringInSetResponse>
    {
        public int Position { get; set; }

        public int StringsSetId { get; set; }

        public int StringId { get; set; }
    }
}
