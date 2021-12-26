using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class RemoveStringsSetRequest : RequestBase<StringsSet>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
