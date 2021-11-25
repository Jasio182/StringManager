using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyStringInSetRequest : RequestBase<StringInSet>
    {
        public int Id { get; set; }

        public int? Position { get; set; }

        public int? StringsSetId { get; set; }

        public int? StringId { get; set; }
    }
}
