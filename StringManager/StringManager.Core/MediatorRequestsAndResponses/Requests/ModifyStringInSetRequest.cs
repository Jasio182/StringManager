using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class ModifyStringInSetRequest : RequestBase<StringInSet>
    {
        public int Id { get; set; }

        public int? Position { get; set; }

        public int? StringsSetId { get; set; }

        public int? StringId { get; set; }
    }
}
