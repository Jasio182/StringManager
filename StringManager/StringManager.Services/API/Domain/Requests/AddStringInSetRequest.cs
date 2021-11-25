using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddStringInSetRequest : RequestBase<StringInSet>
    {
        public int Position { get; set; }

        public int StringsSetId { get; set; }

        public int StringId { get; set; }
    }
}
