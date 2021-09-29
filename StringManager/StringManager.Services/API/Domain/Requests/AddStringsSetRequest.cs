using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddStringsSetRequest : RequestBase<AddStringsSetResponse>
    {
        public string Name { get; set; }

        public int NumberOfStrings { get; set; }
    }
}
