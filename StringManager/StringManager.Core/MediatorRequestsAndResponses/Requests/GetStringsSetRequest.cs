using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetStringsSetRequest : RequestBase<StringsSet>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
