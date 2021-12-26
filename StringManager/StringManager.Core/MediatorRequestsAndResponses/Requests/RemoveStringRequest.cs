using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class RemoveStringRequest : RequestBase<String>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
