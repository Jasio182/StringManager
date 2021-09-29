using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyStringsSetRequest : RequestBase<ModifyStringsSetResponse>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? NumberOfStrings { get; set; }
    }
}
